using Owin;
using Sahurjt.Signalr.Dashboard.Middleware;

namespace Sahurjt.Signalr.Dashboard.Extensions
{
    public static class OwinExtension
    {
        /// <summary>
        /// Extension method of IAppBuilder to expose this library to .Net code.
        /// To use this as middleware call <code> app.UseSignalrDashboard() </code> before signalr middleware.
        /// </summary>
        /// <param name="app">OWIN middleware app</param>
        /// <returns>OWIN middleware app</returns>
        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app)
        {
            app.Use(typeof(SignalrInterceptorMiddleware));

            return app;
        }
    }
}
