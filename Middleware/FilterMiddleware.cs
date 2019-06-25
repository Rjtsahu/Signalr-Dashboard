using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Extensions;
using System;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Middleware
{

    internal abstract class FilterMiddleware : OwinMiddleware
    {
        private readonly string _urlStartSegment;

        public FilterMiddleware(OwinMiddleware next, string urlStartSegment) : base(next)
        {
            _urlStartSegment = urlStartSegment;
        }

        public abstract Task BeforeNextPipeline(IOwinContext owinContext);

        public abstract Task AfterNextPipeline(IOwinContext owinContext, TimeSpan pipelineProcessingTime);

        public override async Task Invoke(IOwinContext environment)
        {
            DateTime startTime = DateTime.UtcNow;
            environment.SetRequestId(Guid.NewGuid().ToString());

            if (ShouldRequestBeProcessed(environment.Request))
            {
                await BeforeNextPipeline(environment);
                startTime = DateTime.UtcNow;
            }

            await Next.Invoke(environment);

            if (ShouldRequestBeProcessed(environment.Request))
            {
                var processingTime = DateTime.UtcNow.Subtract(startTime);
                await AfterNextPipeline(environment, processingTime);
            }
        }

        private bool ShouldRequestBeProcessed(IOwinRequest owinRequest)
        {
            return owinRequest.Path.StartsWithSegments(PathString.FromUriComponent(_urlStartSegment));
        }
    }
}
