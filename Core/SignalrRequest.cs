using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Extensions;
using System;
using Sahurjt.Signalr.Dashboard.Core.Message;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal enum RequestType
    {
        Negotiate,
        Connect,
        Start,
        Abort,
        Reconnect,
        Send,
        Poll,
        Ping,
        None
    };

    internal class SignalrRequest
    {

        public readonly IOwinContext OwinContext;
        private RequestType? _currentRequestType { get; set; }
        public readonly RequestQueryCollection QueryCollection;

        public RequestType Type
        {
            get
            {
                if (_currentRequestType == null)
                {
                    _currentRequestType = GetRequestType();
                }
                return (RequestType)_currentRequestType;
            }
        }

        public readonly string OwinRequestId;

        public SignalrRequest(IOwinContext owinContext)
        {
            OwinContext = owinContext;
            OwinRequestId = owinContext.GetRequestId();
            QueryCollection = new RequestQueryCollection(OwinContext.Request.Uri);
        }


        private RequestType GetRequestType()
        {
            var path = OwinContext.Request.Path;
            var lastSegment = path.Value.Substring(path.Value.LastIndexOf('/') + 1);

            if (Enum.TryParse<RequestType>(lastSegment, true, out var result))
            {
                return result;
            }
            return RequestType.None;

        }

    }
}
