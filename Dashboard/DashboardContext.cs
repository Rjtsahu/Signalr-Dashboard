using Microsoft.Owin;

namespace Sahurjt.Signalr.Dashboard.Dashboard
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
