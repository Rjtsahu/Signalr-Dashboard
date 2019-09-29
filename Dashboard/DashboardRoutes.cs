using SignalrDashboard.Dashboard.Dispatchers;

namespace SignalrDashboard.Dashboard
{
    internal static class DashboardRoutes
    {
        public static RouteCollection Routes { get; }

        static DashboardRoutes()
        {
            Routes = new RouteCollection();

            Routes.AddRoute("/js/(?<FileName>.+)", new RawContentDispatcher(
                "application/javascript",
                GetResourcePath("js")
                ));

            Routes.AddRoute("/css/(?<FileName>.+)", new RawContentDispatcher(
                  "text/css",
                    GetResourcePath("css")
                    ));
        }

        private static string GetResourcePath(string contentType)
        {
            return $"{ typeof(DashboardRoutes).Namespace}.Content.{contentType}";
        }
    }
}
