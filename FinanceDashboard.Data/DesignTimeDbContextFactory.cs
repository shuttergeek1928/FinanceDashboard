using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FinanceDashboard.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FinanceDashboardContext>
    {
        public FinanceDashboardContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();

            var builder = new DbContextOptionsBuilder<FinanceDashboardContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            builder.UseSqlServer(connectionString);

            return new FinanceDashboardContext(builder.Options);
        }
    }
}
