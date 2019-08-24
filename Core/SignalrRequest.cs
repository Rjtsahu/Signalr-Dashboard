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

        public readonly IOwinContext _owinContext;
        private RequestType? _currentRequestType { get; set; }
        public RequestQueryCollection RequestQuery;

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
            _owinContext = owinContext;
            OwinRequestId = owinContext.GetRequestId();
        }


        private RequestType GetRequestType()
        {
            var path = _owinContext.Request.Path;
            var lastSegment = path.Value.Substring(path.Value.LastIndexOf('/') + 1);

            if (Enum.TryParse<RequestType>(lastSegment ?? "", true, out var result))
            {
                return result;
            }
            return RequestType.None;

        }

    }
}
