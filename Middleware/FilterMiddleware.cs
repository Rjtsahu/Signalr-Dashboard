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
                startTime = DateTime.UtcNow;

                // Buffer the response
                var stream = environment.Response.Body;
                var buffer = new MemoryStream();
                environment.Response.Body = buffer;

                await Next.Invoke(environment);

                buffer.Seek(0, SeekOrigin.Begin);
                var reader = new StreamReader(buffer);
                string responseBody = reader.ReadToEnd();

                buffer.Seek(0, SeekOrigin.Begin);
                buffer.CopyTo(stream);

                var processingTime = DateTime.UtcNow.Subtract(startTime);
                // set response body as env key
                environment.Set("responseBody", responseBody);

                await AfterNextPipeline(environment, processingTime);
            }
            else
            {
                await Next.Invoke(environment);
            }

        }

        private bool ShouldRequestBeProcessed(IOwinRequest owinRequest)
        {
            return owinRequest.Path.StartsWithSegments(PathString.FromUriComponent(_urlStartSegment));
        }
    }
}
