using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using Sahurjt.Signalr.Dashboard.Core;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.Middleware
{

    internal class SignalrInterceptorMiddleware : FilterMiddleware
    {
        private readonly InterceptorConfiguration configuration;

        public SignalrInterceptorMiddleware(OwinMiddleware next, string signalrUrl) : base(next, signalrUrl)
        {
            configuration = DashboardGlobal.Configuration;
        }

        public override Task BeforeNextPipeline(IOwinContext owinContext)
        {
            LogHelper.Log("Entering BeforeNextPipeline ", owinContext.Get<string>(_environmentRequestId));
            return Task.CompletedTask;
        }

        public override Task AfterNextPipeline(IOwinContext owinContext, TimeSpan pipelineProcessingTime)
        {
            LogHelper.Log("Entering AfterNextPipeline timespan: " + pipelineProcessingTime, owinContext.Get<string>(_environmentRequestId));
            return Task.CompletedTask;
        }

    }
}
