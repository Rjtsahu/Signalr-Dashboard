using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Owin;
using SignalrDashboard.Dashboard;
using SignalrDashboard.Helpers;

namespace SignalrDashboard.Middleware
{

    internal class SignalrDashboardMiddleware : OwinMiddleware
    {
        private readonly string dashboardUrlStartSegment;

        public SignalrDashboardMiddleware(OwinMiddleware next, string dashboardUrl) : base(next)
        {
            dashboardUrlStartSegment = dashboardUrl;
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

            ////var s = DashboardGlobal.ServiceResolver.GetService<ISqlOperation>();
            ////s.Execute(ExecuteSqlQuery.Create_DatabaseTables);
            ////var d = new SessionDto();
            ////var res = d.GetAll();

            var r = DashboardRoutes.Routes.FindDispatcherForRoute(environment.Request.Path.Value);
          // var res = r.Item1.Dispatch(new DashboardContext(environment));
        }

    }
}
