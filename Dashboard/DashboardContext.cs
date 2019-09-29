using Microsoft.Owin;

namespace SignalrDashboard.Dashboard
{
    internal class DashboardContext
    {
        public DashboardRequest Request { get; private set; }
        public DashboardResponse Response { get; private set; }

        public DashboardContext(IOwinContext owinContext)
        {
            Request = new DashboardRequest(owinContext);
            Response = new DashboardResponse(owinContext);
        }
    }
}
