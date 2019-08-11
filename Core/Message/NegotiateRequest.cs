using System;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal class NegotiateRequest : AbstractRequestQuery
    {

        public string Transport { get; set; }

        public override RequestType RequestQueryType => RequestType.Negotiate;

        public NegotiateRequest(Uri requestUri) : base(requestUri) { }

        protected override object GetObject()
        {
            return this;
        }
    }
}
