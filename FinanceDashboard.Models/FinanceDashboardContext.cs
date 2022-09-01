using FinanceDashboard.Utilities.EncryptorsDecryptors;
using FinanceDashboard.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceDashboard.Data.SqlServer
{
    public class FinanceDashboardContext : DbContext
    {
        //private readonly IPasswordMethods _passwordMethods;
        private readonly string _password;
        private readonly string _passwordHash;
        private readonly string _salt;
        private readonly IConfiguration _configuration;

        //public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options, IPasswordMethods passwordMethods, IConfiguration configuration) : base(options)
        //{
        //    _salt = "==4d8dh51d9c";
        //    _password = "Password1!";
        //    _passwordMethods = passwordMethods;
        //    _passwordHash = _passwordMethods.GetHash(_password, _salt);
        //    _configuration = configuration;
        //}

        public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options) : base(options)
        {
            _salt = "==4d8dh51d9c";
            _password = "Password1!";
            _passwordHash = "iuqagbrdfikhwboarnown;fmlmpqwjpmlml;m;'qe65464";
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(table => new
            {
                table.AccountId,
                table.Id
            });

            builder.Entity<User>().HasAlternateKey(c => c.Email).HasName("AlternateKey_Email");

            builder.Entity<User>().HasData(
                new User()
                {
                    AccountId = 1,
                    Id = Guid.NewGuid(),
                    Name = "Atishay Vishwakarma",
                    FirstName = "Atishay",
                    LastName = "Vishwakarma",
                    Email = "atishay1928@outlook.com",
                    PasswordHash = _passwordHash,
                    HashingSalt = _salt,
                    PasswordHashHistory = _passwordHash + ",",
                    MobileNumber = "9827766387",
                    CreatedOn = DateTime.Now
                });
        }
    }
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceDashboardContext>
    {
        public FinanceDashboardContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../FinanceDashboard.Models/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<FinanceDashboardContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            builder.UseSqlServer(connectionString);
            return new FinanceDashboardContext(builder.Options);
        }
    }
}