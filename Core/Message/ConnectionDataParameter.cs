using Newtonsoft.Json;

namespace SignalrDashboard.Core.Message
{
    /// <summary>
    /// This class represents connectionData parameter which is sent in almost every request in QueryString.
    /// This model generally holds name of hub.
    /// Other relavent information should be added later as per need.
    /// </summary>
    internal class ConnectionDataParameter
    {
        [JsonProperty("name")]
        public string HubName { get; set; }
    }
}
