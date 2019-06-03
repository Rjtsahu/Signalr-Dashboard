using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.Middleware
{

    internal class SignalrInterceptorMiddleware : OwinMiddleware
    {
        private readonly string signalrUrlStartSegment;
        private readonly InterceptorConfiguration configuration;

        public SignalrInterceptorMiddleware(OwinMiddleware next, string signalrUrl, InterceptorConfiguration config) : base(next)
        {
            signalrUrlStartSegment = signalrUrl;
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

            LogHelper.Log(" signalrUrlStartSegment ", signalrUrlStartSegment);

            LogHelper.Log(" request url ", environment.Request.Path);
            await Next.Invoke(environment);
            LogHelper.Log($" request url  ", environment.Response.StatusCode);

            LogHelper.Log("exiting");
        }
    }
}
