using FinanceDashboard.Data.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FinanceDashboard.Data
{
    public class FinanceDashboardContext : DbContext
    {
        public FinanceDashboardContext(DbContextOptions<FinanceDashboardContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(table => new {
                table.AccpuntId,
                table.Id
            });
        }
    }
}