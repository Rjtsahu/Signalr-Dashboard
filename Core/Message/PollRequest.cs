using System;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal class PollRequest : AbstractRequestQuery
    {

        public string Transport { get; set; }

        public string ConnectionToken { get; set; }

        public override RequestType RequestQueryType => RequestType.Poll;

        public PollRequest(Uri requestUri) : base(requestUri) { }

        protected override object GetObject()
        {
            return this;
        }
    }
}
