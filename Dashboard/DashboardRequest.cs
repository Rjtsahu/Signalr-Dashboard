using Microsoft.Owin;

namespace SignalrDashboard.Dashboard
{
    internal sealed class DashboardRequest
    {
        private readonly IOwinContext _context;

        public DashboardRequest(IOwinContext owinContext)
        {
            _context = owinContext;
        }

        public string Method => _context.Request.Method;
        public string Path => _context.Request.Path.Value;
        public string PathBase => _context.Request.PathBase.Value;
        public string LocalIpAddress => _context.Request.LocalIpAddress;
        public string RemoteIpAddress => _context.Request.RemoteIpAddress;

        public string GetQuery(string key) => _context.Request.Query[key];
    }
}
