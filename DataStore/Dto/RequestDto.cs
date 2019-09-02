namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal class RequestDto :IDataTableObject
    {
        public int RequestId { get; set; }
        public string OwinRequestId { get; set; }
        public string SessionId { get; set; }
        public string RequestUrl { get; set; }
        public string RemoteIp { get; set; }
        public int RemotePort { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
        public string RequestContentType { get; set; }
        public string RequestBody { get; set; }
        public string Protocol { get; set; }
        public string QueryString { get; set; }
        public string User { get; set; }
        public int RequestTimeStamp { get; set; }
        public int ResponseTimeStamp { get; set; }
        public int RequestLatency { get; set; }
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
        public bool IsWebSocketRequest { get; set; }
        public string RequestType { get; set; }

        public string TableName => "Request";
    }

}