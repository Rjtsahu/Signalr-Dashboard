namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal class SessionDto : IDataTableObject
    {
        public int SessionId { get; set; }
        public string ConnectionToken { get; set; }
        public string ConnectionId { get; set; }
        public bool IsCompleted { get; set; }
        public string StartTimeStamp { get; set; }
        public string FinishTimeStamp { get; set; }
        public string NegotiateData { get; set; }

        public string TableName => "Session";

    }
}
