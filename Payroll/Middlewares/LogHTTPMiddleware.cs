namespace Payroll.Middlewares
{
    public static class LogHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogHTTPMiddleware>();
        }
    }

    public class LogHTTPMiddleware
    {
        private readonly RequestDelegate _siguiente;
        private readonly ILogger<LogHTTPMiddleware> _logger;
        public LogHTTPMiddleware(RequestDelegate siguiente, ILogger<LogHTTPMiddleware> logger)
        {
            _siguiente = siguiente;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            await _siguiente(contexto);
            _logger.LogInformation(contexto.Request.Scheme + "://" + contexto.Request.Host + contexto.Request.Path);
        }
    }
}