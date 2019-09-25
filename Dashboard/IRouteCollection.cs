using System;
using System.Text.RegularExpressions;

namespace SignalrDashboard.Dashboard
{
    internal interface IRouteCollection
    {
        void AddRoute(string pathPattern, IDashboardDispatcher dashboardDispatcher);

        Tuple<IDashboardDispatcher, Match> FindDispatcherForRoute(string path);
    }
}
