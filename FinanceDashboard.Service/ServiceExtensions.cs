using FinanceDashboard.Data.SqlServer;
using FinanceDashboard.Data.SqlServer.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;

namespace FinanceDashboard.Service
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection Services)
        {
            var builder = Services.AddIdentityCore<ApiUser>(x =>
            {
                x.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), Services);
            builder.AddEntityFrameworkStores<FinanceDashboardContext>().AddDefaultTokenProviders();
        }
    }
}
