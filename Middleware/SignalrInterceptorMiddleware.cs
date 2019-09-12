using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Core;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
    internal class SignalrInterceptorMiddleware : FilterMiddleware
    {
        public object LOgHelper { get; private set; }

        public SignalrInterceptorMiddleware(OwinMiddleware next, string signalrUrl) : base(next, signalrUrl) { }


        public override Task BeforeNextPipeline(IOwinContext owinContext)
        {
            try
            {
                // TODO : use DI instead for unit testing
                new DefaultSignalrInterceptor(owinContext).InvokeRequestMethod();
            }
            catch (Exception e)
            {
                LogHelper.Log($"Exception_BeforeNextPipeline: {e.Message} {e.StackTrace}");
            }

            return Task.CompletedTask;
        }
        
        public override Task AfterNextPipeline(IOwinContext owinContext, TimeSpan pipelineProcessingTime)
        {
            try
            {
                new DefaultSignalrInterceptor(owinContext, pipelineProcessingTime).InvokeRequestMethod();
            }
            catch (Exception e)
            {
                LogHelper.Log($"Exception_AfterNextPipeline: {e.Message} {e.StackTrace}");
            }

            return Task.CompletedTask;
        }

    }
}
