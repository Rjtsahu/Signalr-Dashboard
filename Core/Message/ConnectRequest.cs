using System;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal class ConnectRequest : AbstractRequestQuery
    {
        public string Transport { get; set; }

        public string ConnectionToken { get; set; }

        public override RequestType RequestQueryType => RequestType.Connect;

        public ConnectRequest(Uri requestUri) : base(requestUri) { }

        protected override object GetObject()
        {
            return this;
        }
    }
}
