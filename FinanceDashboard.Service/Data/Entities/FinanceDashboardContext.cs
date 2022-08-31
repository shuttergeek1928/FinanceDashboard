using FinanceDashboard.Service.Data.Entities;
using FinanceDashboard.Service.EncryptorsDecryptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinanceDashboard.Service.Data.Entities
{
    public class FinanceDashboardContext : DbContext
    {
        private readonly IPasswordMethods _passwordMethods;
        private readonly string _password; 
        private readonly string _passwordHash;
        private readonly string _salt;
        public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options, IPasswordMethods passwordMethods) : base(options)
        {
            _salt = "==4d8dh51d9c";
            _password = "Password1!";
            _passwordMethods = passwordMethods;
            _passwordHash = _passwordMethods.GetHash(_password, _salt);
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(table => new {
                table.AccpuntId,
                table.Id
            });

            builder.Entity<User>().HasData(
                new User()
                {
                    AccpuntId = 1,
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
}