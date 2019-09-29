using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SignalrDashboard.Dashboard
{
    internal class RouteCollection 
    {
        private readonly IDictionary<string, IDashboardDispatcher> _dispatchers = new Dictionary<string, IDashboardDispatcher>();

        /// <summary>
        /// Adds a route pattern to route-collection
        /// </summary>
        /// <param name="pathPattern">url path pattern.</param>
        /// <param name="dashboardDispatcher">dispatcher class to be executed when a path matches.</param>
        public void AddRoute(string pathPattern, IDashboardDispatcher dashboardDispatcher)
        {
            if (string.IsNullOrEmpty(pathPattern)) throw new ArgumentNullException("pathPattern");

            if (dashboardDispatcher == null) throw new ArgumentNullException("dashboardDispatcher");

            _dispatchers.Add(pathPattern, dashboardDispatcher);
        }


        /// <summary>
        /// Finds a matching path from the route collection using regEx.
        /// </summary>
        /// <param name="path">url path</param>
        /// <returns>tuple of dispatcher to be executed and matched groups if any.</returns>
        public Tuple<IDashboardDispatcher, Match> FindDispatcherForRoute(string path)
        {
            if (path.Length == 0) path = "/";
            else if (path.Length > 1) path = path.TrimEnd('/');

            foreach (var dispatcher in _dispatchers)
            {
                var pattern = dispatcher.Key;

                if (!pattern.StartsWith("^", StringComparison.OrdinalIgnoreCase))
                    pattern = "^" + pattern;
                if (!pattern.EndsWith("$", StringComparison.OrdinalIgnoreCase))
                    pattern += "$";

                var match = Regex.Match(
                    path,
                    pattern,
                    RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (match.Success)
                {
                    return new Tuple<IDashboardDispatcher, Match>(dispatcher.Value, match);
                }
            }

            return null;
        }
    }
}
