using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
    public class WebSocketProxyMiddleware : OwinMiddleware

    {
        // private static readonly string _wsServerPath = "wss://localhost:1337/";
        private static readonly string _wsServerPath = "ws://localhost:50413/signalr/signalr/connect";
        public WebSocketProxyMiddleware(
            OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext owinContext)
        {
            var context = owinContext.Get<HttpContextBase>(
                typeof(HttpContextBase).FullName);
            var httpContext = context.ApplicationInstance.Context;
            var isServer = owinContext.Request.Headers["isServer"];

            if (httpContext.IsWebSocketRequest && isServer != "true")
            {
                await ProxyOutToWebSocket(context).ConfigureAwait(false);
            }
            else
            {
                await Next.Invoke(owinContext).ConfigureAwait(false);
            }
        }

        private Task ProxyOutToWebSocket(HttpContextBase context)
        {
            var relativePath = context.Request.Path.ToString();
            var upstreamUri = "";

            //var uriWithPath = new Uri(
            //    upstreamUri.Uri,
            //    relativePath.Length >= 0 ? relativePath : "");

            var uriBuilder = new UriBuilder(new Uri(_wsServerPath))
            {
                Query = context.Request.RawUrl.Split('?')[1]
            };


            return AcceptProxyWebSocketRequest(context, uriBuilder.Uri);
        }

        private async Task AcceptProxyWebSocketRequest(HttpContextBase context, Uri upstreamUri)
        {
            using (var client = new ClientWebSocket())
            {
                foreach (var protocol in context.WebSocketRequestedProtocols)
                {
                    client.Options.AddSubProtocol(protocol);
                }

               client.Options.SetRequestHeader("isServer", "true");

                // _customizeWebSocketClient(new WebSocketClientOptions(client.Options, context));

                try
                {
                    await client.ConnectAsync(upstreamUri, new CancellationToken()).ConfigureAwait(false);
                }
                catch (WebSocketException ex)
                {
                    context.Response.StatusCode = 400;
                    return;
                }

                context.AcceptWebSocketRequest(async (wsContext) =>
                {
                    var ws = wsContext.WebSocket;
                    await Task.WhenAll(
                        //PumpWebSocket(client, ws, 1024*1024, new CancellationToken()),
                        PumpWebSocket(ws, client, 1024*1024, new CancellationToken())
                        );
                });

            }
        }
        //@Todo: rajat 30/05/2019 Made some code changes in this function please find out 
        private static async Task PumpWebSocket(WebSocket source, WebSocket destination, int bufferSize, CancellationToken cancellationToken)
        {
            if (bufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            }

            var buffer = new byte[bufferSize];
            while (true)
            {
                WebSocketReceiveResult result;
                try
                {
                    result = await source
                        .ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    await destination.CloseOutputAsync(
                        WebSocketCloseStatus.EndpointUnavailable,
                        "Endpoint unavailable",
                        cancellationToken)
                        .ConfigureAwait(false);
                    return;
                }
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await destination
                        .CloseOutputAsync(source.CloseStatus.Value, source.CloseStatusDescription, cancellationToken)
                        .ConfigureAwait(false);
                    return;
                }
                await destination.SendAsync(
                    new ArraySegment<byte>(buffer, 0, result.Count),
                    result.MessageType,
                    result.EndOfMessage,
                    cancellationToken)
                    .ConfigureAwait(false);
            }
        }
    }
}
