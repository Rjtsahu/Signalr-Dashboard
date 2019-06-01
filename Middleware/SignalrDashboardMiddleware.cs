using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin;

namespace Sahurjt.Signalr.Dashboard.Middleware
{

    internal class SignalrDashboardMiddleware : OwinMiddleware
    {

        public SignalrDashboardMiddleware(OwinMiddleware next) : base(next)
        {
        }

        /// <summary>
        /// This is a middleware method will be invoked when a request is accepted in pipeline.
        /// </summary>
        /// <param name="environment">Environment detail for this pipelined request.</param>
        /// <returns>async task for next middleware in pipeline</returns>
        public override async Task Invoke(IOwinContext environment)
        {
            Console.WriteLine("entering");
            await Next.Invoke(environment);
            Console.WriteLine("exiting");
        }
    }
}
