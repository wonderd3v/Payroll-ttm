using Microsoft.Extensions.DependencyInjection;
using Payroll.Model.Repositories;

namespace Payroll.Model.IoC
{
    public static class ModelRegistry
    {
        public static void AddModelRegistry(this IServiceCollection services)
        {
            services.AddTransient<IExamplePersonRepository, ExamplePersonRepository>();
        }
    }
}