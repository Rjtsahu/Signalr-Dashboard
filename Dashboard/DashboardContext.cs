using Microsoft.Owin;

namespace SignalrDashboard.Dashboard
{
    internal class DashboardContext
    {
        private readonly OwinContext _owinContext;

        public DashboardContext(OwinContext owinContext)
        {
            _owinContext = owinContext;
        }
    }
}
