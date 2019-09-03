using Newtonsoft.Json;
using Sahurjt.Signalr.Dashboard.Core.Message.Response;
using Sahurjt.Signalr.Dashboard.DataStore;
using Sahurjt.Signalr.Dashboard.DataStore.Dto;
using Sahurjt.Signalr.Dashboard.Extensions;
using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultDataTracing : IDataTracing
    {
        private readonly ISqlOperation _sqlOperation;

        // TODO : make DI more generic
        public DefaultDataTracing(ISqlOperation sqlOperation)
        {
            _sqlOperation = sqlOperation;
        }

        public bool AddRequestTrace(string owinRequestId, SignalrRequest signalrRequest)
        {
            var owinRequest = signalrRequest.OwinContext.Request;

            var sessionObj = _sqlOperation.Select<SessionDto>(SelectSqlQuery.GetSingle_Session_By_ConnectionToken,
                signalrRequest.QueryCollection.ConnectionToken);

            if (sessionObj == null) return false;

            var requestEntity = new RequestDto
            {
                SessionId = sessionObj.SessionId,
                OwinRequestId = owinRequestId,
                RequestUrl = owinRequest.Uri.AbsoluteUri,
                RemoteIp = owinRequest.RemoteIpAddress,
                RemotePort = owinRequest.RemotePort ?? 0,
                ServerIp = owinRequest.LocalIpAddress,
                ServerPort = owinRequest.LocalPort ?? 0,
                RequestContentType = owinRequest.ContentType,
                RequestBody = owinRequest.ReadBody(),
                Protocol = owinRequest.Protocol,
                QueryString = owinRequest.QueryString.Value,
                User = owinRequest.User?.Identity?.Name,
                RequestTimeStamp = DateTime.UtcNow.Ticks,
                IsWebSocketRequest = signalrRequest.QueryCollection.Transport == "webSockets",
                RequestType = signalrRequest.Type.ToString()
            };

            return requestEntity.Save(_sqlOperation);
        }

        public bool CompleteRequestTrace(string owinRequestId, SignalrRequest signalrRequest)
        {
            return false;
        }

        public void FinishSession(string connectionToken)
        {
            _sqlOperation.ExecuteAsync(ExecuteSqlQuery.Update_SessionOnCompleted, 1,
               DateTime.UtcNow.Ticks, connectionToken);
        }

        public void StartSession(SignalrRequest signalrRequest)
        {
            /// starts session for a particular browser client
            var jsonData = signalrRequest.ResponseBody;

            if (string.IsNullOrEmpty(jsonData)) return;

            var negotiateResponseObj = JsonConvert.DeserializeObject<NegotiateResponse>(jsonData);

            var sessionEntity = new SessionDto
            {
                ConnectionId = negotiateResponseObj.ConnectionId,
                ConnectionToken = negotiateResponseObj.ConnectionToken,
                NegotiateData = jsonData,
                StartTimeStamp = DateTime.UtcNow.Ticks,
                FinishTimeStamp = 0
            };

            sessionEntity.Save(_sqlOperation);
        }
    }
}
