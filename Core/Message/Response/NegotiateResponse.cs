
namespace SignalrDashboard.Core.Message.Response
{
    internal class NegotiateResponse 
    {
        public string ConnectionId { get; set; }
        public double ConnectionTimeout { get; set; }
        public string ConnectionToken { get; set; }
        public double DisconnectTimeout { get; set; }
        public double KeepAliveTimeout { get; set; }
        public double LongPollDelay { get; set; }
        public string ProtocolVersion { get; set; }
        public double TransportConnectTime { get; set; }
        public bool TryWebSockets { get; set; }
        public string Url { get; set; }
    }
}
