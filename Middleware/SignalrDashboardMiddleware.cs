using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
     using AppFunc = Func<IDictionary<string, object>, Task>;

    internal class SignalrDashboardMiddleware
    {
        private readonly AppFunc next;

        public SignalrDashboardMiddleware(AppFunc next)
        {
            this.next = next;
        }

        /// <summary>
        /// This is a middleware method will be invoked when a request is accepted in pipeline.
        /// </summary>
        /// <param name="environment">Environment detail for this pipelined request.</param>
        /// <returns>async task for next middleware in pipeline</returns>
        public async Task Invoke(IDictionary<string, object> environment)
        {
            Console.WriteLine("entering");
            await next.Invoke(environment);
            Console.WriteLine("exiting");
        }
    }
}
