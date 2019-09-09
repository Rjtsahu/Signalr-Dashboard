using Newtonsoft.Json;
using Sahurjt.Signalr.Dashboard.Core.Message;
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

        public bool AddRequestTrace(SignalrRequest signalrRequest)
        {
            return AddRequestTrace(signalrRequest, signalrRequest.QueryCollection.ConnectionToken);
        }

        private bool AddRequestTrace(SignalrRequest signalrRequest, string connectionToken)
        {
            var sessionObj = _sqlOperation.Select<SessionDto>(SelectSqlQuery.GetSingle_Session_By_ConnectionToken,
               connectionToken);

            if (sessionObj == null) return false;


            var owinRequest = signalrRequest.OwinContext.Request;

            var requestEntity = new RequestDto
            {
                SessionId = sessionObj.SessionId,
                OwinRequestId = signalrRequest.OwinRequestId,
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

        public void CompleteRequestTrace(SignalrRequest signalrRequest)
        {
            _sqlOperation.ExecuteAsync(ExecuteSqlQuery.Update_RequestOnCompleted, DateTime.UtcNow.Ticks, 0,
                signalrRequest.OwinContext.Response.StatusCode, signalrRequest.ResponseBody, signalrRequest.OwinRequestId);
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

            var saved = sessionEntity.Save(_sqlOperation);
            if (saved)
            {
                AddRequestTrace(signalrRequest, negotiateResponseObj.ConnectionToken);
                AddHubData(sessionEntity.ConnectionId, signalrRequest.QueryCollection.ConnectionData);
            }
        }


        private void AddHubData(string connectionId, string hubData)
        {
            var updateSql = @"UPDATE Session SET HubData =@HubData WHERE ConnectionId = @ConnectionId";

            _sqlOperation.ExecuteAsync(updateSql, hubData, connectionId);
        }
    }
}
