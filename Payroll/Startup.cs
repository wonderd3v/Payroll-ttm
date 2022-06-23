using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Net.Http.Headers;
using Payroll.Bl.Settings;
using Payroll.Middlewares;
using Payroll.Model.IoC;
using Payroll.Services.IoC;
using Payroll.Settings;

namespace Payroll
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region CORS

            services.AddCors(options =>
            {
                options.AddPolicy("MainPolicy",
                      builder =>
                      {
                          builder
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();

                          //TODO: remove this line for production
                          builder.SetIsOriginAllowed(x => true);
                      });
            });

            #endregion

            #region External Dependencies
            services.SettingSqlServerDbContext(Configuration.GetConnectionString("DefaultConnection"));

            services.AddControllers(options => options.EnableEndpointRouting = false)
            .SettingFluentValidation()
            .AddOData(options => 
            {
                options.Select().Expand().Filter().OrderBy().SetMaxTop(100).Count();
            })
            .AddNewtonsoftJson();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });

            services.SettingAutoMapper();

            services.AddEndpointsApiExplorer();
            #endregion

            #region API Libraries
            services.SettingSwagger();
            services.SettingHealthCheck();
            #endregion

            #region App Registries
            services.AddModelRegistry();
            services.AddServiceRegistry(Configuration);
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseLogHTTP();

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {

            }

            app.UseAppSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MainPolicy");  

            app.UseAuthorization();

            app.UseEndpoints(endPoints => 
            {
                endPoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endPoints.MapHealthChecksUI();
                endPoints.MapControllers();
            });
        }
    }
}