using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Owin.WebSocket;

namespace Sahurjt.Signalr.Dashboard.Middleware
{
    class WebSocketMiddleware : WebSocketConnection
    {
        public override void OnClose(WebSocketCloseStatus? closeStatus, string closeStatusDescription)
        {
            base.OnClose(closeStatus, closeStatusDescription);
        }
  
        public override Task OnMessageReceived(ArraySegment<byte> message, WebSocketMessageType type)
        {
            return base.OnMessageReceived(message, type);
        }
        public override void OnOpen()
        {
            base.OnOpen();
        }

        public override void OnReceiveError(Exception error)
        {
            base.OnReceiveError(error);
        }

    }
}
