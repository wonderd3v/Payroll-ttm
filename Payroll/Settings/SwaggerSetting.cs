using Microsoft.OpenApi.Models;

namespace Payroll.Settings
{
    public static class SwaggerSetting
    {
        public static IServiceCollection SettingSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Base Template API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Solvex Dominicana",
                        Email = string.Empty,
                        Url = new Uri("https://solvex.com.do/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkShop API V1");
            });
            return app;
        }
    }
}