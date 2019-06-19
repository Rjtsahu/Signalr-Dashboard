using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using Sahurjt.Signalr.Dashboard.Core;
using Sahurjt.Signalr.Dashboard.DataStore;
using Sahurjt.Signalr.Dashboard.DataStore.Dto;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.Middleware
{

    internal class SignalrDashboardMiddleware : OwinMiddleware
    {
        private readonly string dashboardUrlStartSegment;
        private readonly InterceptorConfiguration configuration;

        public SignalrDashboardMiddleware(OwinMiddleware next, string dashboardUrl, InterceptorConfiguration config) : base(next)
        {
            dashboardUrlStartSegment = dashboardUrl;
            configuration = config;
        }

        /// <summary>
        /// This is a middleware method will be invoked when a request is accepted in pipeline.
        /// </summary>
        /// <param name="environment">Environment detail for this pipelined request.</param>
        /// <returns>async task for next middleware in pipeline</returns>
        public override async Task Invoke(IOwinContext environment)
        {
            LogHelper.Log("entering");

            LogHelper.Log(" signalrUrlStartSegment", dashboardUrlStartSegment);

            LogHelper.Log("  request url ", environment.Request.Path);
            await Next.Invoke(environment);
            LogHelper.Log($"  request url ", environment.Response.StatusCode);

            LogHelper.Log("exiting");

            var s = DashboardGlobal.ServiceResolver.GetService<ISqlOperation>();
            s.Execute(ExecuteSqlQuery.Create_DatabaseTables);
            var d = new SessionDto();
            var res = d.GetAll();
        }

    }
}
