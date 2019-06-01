using Owin;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Middleware;
namespace Sahurjt.Signalr.Dashboard.Extensions
{
    public static class OwinExtension
    {
        private static readonly string _defaultDashboardSegment = "/dashboard";
        private static readonly string _defaultSignalrSegment = "/signalr";

        /// <summary>
        /// Extension method of IAppBuilder to expose this library to .Net code.
        /// To use this as middleware call <code> app.UseSignalrDashboard() </code> before signalr middleware.
        /// </summary>
        /// <param name="app">OWIN middleware app</param>
        /// <returns>OWIN middleware app</returns>
        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app)
        {
            return UseSignalrDashboard(app, _defaultDashboardSegment);
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string url)
        {
            app.RunInterceptor();

            app.MapWhen(p => p.Request.Path.StartsWithSegments(PathString.FromUriComponent(url)), subApp => subApp.RunDashboard());

            return app;
        }

        private static IAppBuilder RunDashboard(this IAppBuilder app)
        {
            return app.Use(typeof(SignalrDashboardMiddleware));
        }
        private static IAppBuilder RunInterceptor(this IAppBuilder app)
        {
            return app.Use<SignalrInterceptorMiddleware>();
        }

    }
}
