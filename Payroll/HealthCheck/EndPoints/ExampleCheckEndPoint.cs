using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Payroll.HealthCheck.EndPoints
{
    public class ExampleCheckEndPoint : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync("https://json-server-apap.herokuapp.com/");

                return result.StatusCode == System.Net.HttpStatusCode.OK 
                    ?
                        await Task.FromResult(new HealthCheckResult 
                        (
                            status: HealthStatus.Healthy
                        ))
                    :
                        await Task.FromResult(new HealthCheckResult 
                        (
                            status: HealthStatus.Unhealthy
                        ));
            }
        }
    }
}