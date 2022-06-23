using Microsoft.Extensions.DependencyInjection;

namespace Payroll.Bl.Settings
{
    public static class AutoMapperSetting
    {
        public static IServiceCollection SettingAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}