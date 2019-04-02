using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    internal class SignalrInterceptorMiddleware
    {
        private AppFunc next;

        public SignalrInterceptorMiddleware(AppFunc next)
        {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            Console.WriteLine("entering");
            await next.Invoke(environment);
            Console.WriteLine("exiting");
        }
    }
}
