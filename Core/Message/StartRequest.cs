using System;

namespace Sahurjt.Signalr.Dashboard.Core.Message
{
    internal class StartRequest : AbstractRequestQuery
    {
        public string Transport { get; set; }

        public string ConnectionToken { get; set; }

        public override RequestType RequestQueryType => RequestType.Start;

        public StartRequest(Uri requestUri) : base(requestUri) { }

        protected override object GetObject()
        {
            return this;
        }
    }
}
