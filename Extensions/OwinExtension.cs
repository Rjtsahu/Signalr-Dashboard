using Owin;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Middleware;
using Sahurjt.Signalr.Dashboard.Core;

namespace Sahurjt.Signalr.Dashboard.Extensions
{
    public static class OwinExtension
    {
        private static readonly string _defaultSignalrRoute = DashboardGlobal.Configuration.DefaultSignalrRoute;
        private static readonly string _defaultDashboardRoute = DashboardGlobal.Configuration.DefaultDashboardRoute;

        private static string _requestIdKey = "requestId";

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

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string dashboardUrl)
        {
            return UseSignalrDashboard(app, dashboardUrl, _defaultSignalrRoute);
        }

        public static IAppBuilder UseSignalrDashboard(this IAppBuilder app, string dashboardUrl, string signalrUrl)
        {
            app.RunInterceptor(signalrUrl);

            app.MapWhen(p =>
            p.Request.Path.StartsWithSegments(PathString.FromUriComponent(dashboardUrl)),
            subApp => subApp.RunDashboard(dashboardUrl));

            return app;
        }

        private static IAppBuilder RunDashboard(this IAppBuilder app, string dashboardUrl)
        {
            return app.Use<SignalrDashboardMiddleware>(dashboardUrl);
        }

        private static IAppBuilder RunInterceptor(this IAppBuilder app, string signalrUrl)
        {
            return app.Use<SignalrInterceptorMiddleware>(signalrUrl);
        }

        public static void SetRequestId(this IOwinContext owinContext, string requestId)
        {
            owinContext.Set<string>(_requestIdKey, requestId);
        }

        public static string GetRequestId(this IOwinContext owinContext)
        {
            return owinContext.Get<string>(_requestIdKey);
        }
    }
}
