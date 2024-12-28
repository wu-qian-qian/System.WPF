using System.Api.MiddleWare;

namespace System.Api.Module
{
    public static class LoggingMiddlewareExpandModule
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SerilogMddleware>();
        }
    }
}
