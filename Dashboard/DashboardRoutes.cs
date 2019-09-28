using SignalrDashboard.Dashboard.Dispatchers;

namespace SignalrDashboard.Dashboard
{
    internal static class DashboardRoutes
    {
        public static RouteCollection Routes { get; }

        static DashboardRoutes()
        {
            Routes = new RouteCollection();

            Routes.AddRoute("/js[0-9]+", new RawContentDispatcher(
                "application/javascript",
                GetResourcePath("js")
                ));

            Routes.AddRoute("/css[0-9]+", new RawContentDispatcher(
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
