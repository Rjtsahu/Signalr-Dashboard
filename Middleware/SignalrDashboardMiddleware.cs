using System.Threading.Tasks;
using Microsoft.Owin;
using SignalrDashboard.Dashboard;

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

            var requestPath = environment.Request.Path.Value.Substring(environment.Request.Path.Value.IndexOf(dashboardUrlStartSegment) + dashboardUrlStartSegment.Length);
            var dispatcher = DashboardRoutes.Routes.FindDispatcherForRoute(requestPath);

            if (dispatcher == null)
            {
                await SendNotFound(environment);
            }
            else
            {
                try
                {
                    await dispatcher.Item1.Dispatch(new DashboardContext(environment));
                }
                catch (ResourceNotExistException e)
                {
                    await SendNotFound(environment, "Not found: " + e._resourceName);
                }

            }
        }

        private async Task SendNotFound(IOwinContext owinContext, string errorMessage = "Resource not found")
        {
            owinContext.Response.StatusCode = 404;
            await owinContext.Response.WriteAsync(errorMessage);
        }
    }
}
