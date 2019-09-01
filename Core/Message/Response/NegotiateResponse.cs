
namespace Sahurjt.Signalr.Dashboard.Core.Message.Response
{
    internal class NegotiateResponse 
    {
        public string ConnectionId { get; set; }
        public int ConnectionTimeout { get; set; }
        public string ConnectionToken { get; set; }
        public int DisconnectTimeout { get; set; }
        public int KeepAliveTimeout { get; set; }
        public int LongPollDelay { get; set; }
        public string ProtocolVersion { get; set; }
        public int TransportConnectTime { get; set; }
        public bool TryWebSockets { get; set; }
        public string Url { get; set; }
    }
}
