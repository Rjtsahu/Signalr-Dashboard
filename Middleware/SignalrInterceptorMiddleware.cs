using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Core;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
    internal class SignalrInterceptorMiddleware : FilterMiddleware
    {

        public SignalrInterceptorMiddleware(OwinMiddleware next, string signalrUrl) : base(next, signalrUrl) { }


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
