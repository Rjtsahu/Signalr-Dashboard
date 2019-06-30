using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using Sahurjt.Signalr.Dashboard.Core;

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
            new DefaultSignalrInterceptor(owinContext).InvokeRequestMethod();

            return Task.CompletedTask;
        }

        public override Task AfterNextPipeline(IOwinContext owinContext, TimeSpan pipelineProcessingTime)
        {
            new DefaultSignalrInterceptor(owinContext, pipelineProcessingTime).InvokeRequestMethod();

            return Task.CompletedTask;
        }

    }
}
