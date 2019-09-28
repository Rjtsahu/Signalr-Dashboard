using Microsoft.Owin;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SignalrDashboard.Dashboard
{
    internal sealed class DashboardResponse
    {
        private readonly IOwinContext _context;

        public DashboardResponse(IOwinContext owinContext)
        {
            _context = owinContext;
        }

        public string ContentType
        {
            get { return _context.Response.ContentType; }
            set { _context.Response.ContentType = value; }
        }

        public int StatusCode
        {
            get { return _context.Response.StatusCode; }
            set { _context.Response.StatusCode = value; }
        }

        public Stream Body => _context.Response.Body;

        public void SetExpire(DateTimeOffset? value)
        {
            _context.Response.Expires = value;
        }

        public Task WriteAsync(string text)
        {
            return _context.Response.WriteAsync(text);
        }
    }
}
