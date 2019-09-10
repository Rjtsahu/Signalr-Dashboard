using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Extensions;
using System;
using System.IO;
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

                environment.Request.Body.Seek(0, SeekOrigin.Begin);
                startTime = DateTime.UtcNow;

                //// Buffer the response
                var responseStream = environment.Response.Body;
                var responseBuffer = new MemoryStream();

                environment.Response.Body = responseBuffer;

                await Next.Invoke(environment);

                responseBuffer.Seek(0, SeekOrigin.Begin);
                responseBuffer.CopyTo(responseStream);

                var processingTime = DateTime.UtcNow.Subtract(startTime);

                await AfterNextPipeline(environment, processingTime);

            }
            else
            {
                await Next.Invoke(environment);
            }

        }

        private bool ShouldRequestBeProcessed(IOwinRequest owinRequest)
        {
            // as of now we cannot intercept connect call on serverSentEvents as it will break the response event pipe
            // need to find alternative solution for this, for now ignoring this request to be processed.

            return owinRequest.Path.StartsWithSegments(PathString.FromUriComponent(_urlStartSegment))
             && !owinRequest.Uri.AbsoluteUri.Contains("connect?transport=serverSentEvents");
        }

    }
}
