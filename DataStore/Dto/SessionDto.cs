namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal class SessionDto : IDataTableObject
    {
        public int SessionId { get; set; }
        public int ConnectionToken { get; set; }
        public int ConnectionId { get; set; }
        public bool IsCompleted { get; set; }
        public int StartTimeStamp { get; set; }
        public int FinishTimeStamp { get; set; }

        public string TableName => "Session";

    }
}
