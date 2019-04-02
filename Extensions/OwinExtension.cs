using Owin;
using Sahurjt.Signalr.Dashboard.Middleware;

namespace Sahurjt.Signalr.Dashboard.Extensions
{
    public static class OwinExtension
    {
        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app)
        {
            app.Use(typeof(SignalrInterceptorMiddleware));

            return app;
        }
    }
}
