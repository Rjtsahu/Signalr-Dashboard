using Owin;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Middleware;
using Sahurjt.Signalr.Dashboard.Configuration;

namespace Sahurjt.Signalr.Dashboard.Extensions
{
    public static class OwinExtension
    {
        private static readonly string _defaultSignalrRoute = "/signalr";
        private static readonly string _defaultDashboardRoute = "/dashboard";



        /// <summary>
        /// Extension method of IAppBuilder to expose this library to .Net code.
        /// To use this as middleware call <code> app.UseSignalrDashboard() </code> before signalr middleware.
        /// </summary>
        /// <param name="app">OWIN middleware app</param>
        /// <returns>OWIN middleware app</returns>
        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app)
        {
            return UseSignalrDashboard(app, _defaultDashboardRoute);
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, InterceptorConfiguration config)
        {
            return UseSignalrDashboard(app, _defaultDashboardRoute, _defaultSignalrRoute, config);
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string dashboardUrl)
        {
            return UseSignalrDashboard(app, dashboardUrl, _defaultSignalrRoute);
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string dashboardUrl, string signalrUrl)
        {
            return UseSignalrDashboard(app, dashboardUrl, signalrUrl, new InterceptorConfiguration());
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string dashboardUrl, string signalrUrl, InterceptorConfiguration config)
        {
            app.RunInterceptor(signalrUrl, config);

            app.MapWhen(p =>
            p.Request.Path.StartsWithSegments(PathString.FromUriComponent(dashboardUrl)),
            subApp => subApp.RunDashboard(dashboardUrl, config));

            return app;

        }

        private static IAppBuilder RunDashboard(this IAppBuilder app, string dashboardUrl, InterceptorConfiguration config)
        {
            return app.Use<SignalrDashboardMiddleware>(dashboardUrl, config);
        }


        private static IAppBuilder RunInterceptor(this IAppBuilder app, string signalrUrl, InterceptorConfiguration config)
        {
            return app.Use<SignalrInterceptorMiddleware>(signalrUrl, config);
        }

    }
}
