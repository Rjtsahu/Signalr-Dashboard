using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.DataStore.Dto
{
    internal class RequestDto : IDataTableObject
    {
        public int RequestId { get; set; }
        public string OwinRequestId { get; set; }
        public int SessionId { get; set; }
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
        public long RequestTimeStamp { get; set; }
        public long ResponseTimeStamp { get; set; }
        public long RequestLatency { get; set; }
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
        public bool IsWebSocketRequest { get; set; }
        public string RequestType { get; set; }

        public string TableName => "Request";

        public bool Save(ISqlOperation sqlOperation)
        {
            var rowAdded = sqlOperation.Execute(ExecuteSqlQuery.InsertRow_Request,OwinRequestId, SessionId, RequestUrl,
                 RemoteIp, RemotePort, ServerIp, ServerPort, RequestContentType, RequestBody, Protocol, QueryString, User,
                 RequestTimeStamp, ResponseTimeStamp, RequestLatency, StatusCode, ResponseBody, IsWebSocketRequest, RequestType);

            return rowAdded == 1;
        }

        public async Task<bool> SaveAsync(ISqlOperation sqlOperation)
        {
            var rowAdded = await sqlOperation.ExecuteAsync(ExecuteSqlQuery.InsertRow_Request, SessionId, RequestUrl,
                 RemoteIp, RemotePort, ServerIp, ServerPort, RequestContentType, RequestBody, Protocol, QueryString, User,
                 RequestTimeStamp, ResponseTimeStamp, RequestLatency, StatusCode, ResponseBody, IsWebSocketRequest, RequestType);

            return rowAdded == 1;
        }
    }

}