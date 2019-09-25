using SignalrDashboard.Configuration;
using System;

namespace SignalrDashboard.Core
{
    public static class DashboardGlobal
    {
        private static readonly Lazy<IServiceResolver> _serviceResolver = new Lazy<IServiceResolver>(() => new DefaultServiceResolver());

        public static IServiceResolver ServiceResolver
        {
            get { return _serviceResolver.Value; }
        }

        public static InterceptorConfiguration Configuration => new InterceptorConfiguration();

    }
}
