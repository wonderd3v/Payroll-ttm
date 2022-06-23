using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Payroll.Model.Context;

namespace Payroll.Bl.Settings
{
    public static class SqlServerDbSetting
    {
        public static IServiceCollection SettingSqlServerDbContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}