using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    public static class DashboardGlobal
    {
        private static readonly Lazy<IServiceResolver> _serviceResolver = new Lazy<IServiceResolver>(() => new DefaultServiceResolver());

        public static IServiceResolver ServiceResolver
        {
            get { return _serviceResolver.Value; }
        }
    }
}
