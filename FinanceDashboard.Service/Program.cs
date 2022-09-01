using FinanceDashboard.Service.EncryptorsDecryptors;
using Microsoft.EntityFrameworkCore;
using FinanceDashboard.Service.Data.IDataController;
using FinanceDashboard.Service.Data.DataController;
using FinanceDashboard.Service;
using FinanceDashboard.Service.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<FinanceDashboardContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<IPasswordMethods, PasswordMethods>();
builder.Services.AddScoped<IUserDataController, UserDataController>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
