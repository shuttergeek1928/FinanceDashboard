using FinanceDashboard.Core.Controllers;
using FinanceDashboard.Data.DataController;
using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Authorization;
using FinanceDashboard.Utilities.EncryptorsDecryptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<FinanceDashboardContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddScoped<IPasswordMethods, PasswordMethods>();
builder.Services.AddScoped<SubscriptionDataController>();
builder.Services.AddScoped<AccountDataController>();
builder.Services.AddScoped<AccountController>();
builder.Services.AddScoped<SubscriptionController>();

//builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers(option =>
{
    option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters().AddJsonOptions(jsonOption =>
{
    jsonOption.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    // set as pascal case
    jsonOption.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<FinanceDashboardContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer  
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["ApiSetting:JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["ApiSetting:JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddMvc().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Finance Dashboard API help page."
    });

    //Using system reflection
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    xmlFilename = xmlFilename.Remove(16, 8);
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    var modules = Assembly.GetExecutingAssembly().GetExportedTypes();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("FinanceDashboard.Service.swagger-ui.custom.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
