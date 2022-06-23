using Microsoft.Extensions.Diagnostics.HealthChecks;
using Payroll.HealthCheck.EndPoints;
using Payroll.Model.Context;

namespace Payroll.Settings
{
    public static class HealthCheckSetting
    {
        public static IServiceCollection SettingHealthCheck(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDbContextCheck<ApplicationDbContext>()
                .AddCheck<ExampleCheckEndPoint>("Example Check EndPoint");

            services.AddHealthChecksUI()
                .AddSqliteStorage($"Data Source=HealthCheck/Db/sqlite.db");

            return services;
        }
    }
}