using Newtonsoft.Json;
using Sahurjt.Signalr.Dashboard.Core.Message.Response;
using Sahurjt.Signalr.Dashboard.DataStore;
using Sahurjt.Signalr.Dashboard.DataStore.Dto;
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
            /// TODO ; IMPL
            return false;
        }

        public bool CompleteRequestTrace(string owinRequestId, SignalrRequest signalrRequest)
        {
            /// TODO ; IMPL
            return false;
        }

        public void FinishSession(string connectionToken)
        {
            _sqlOperation.ExecuteAsync(ExecuteSqlQuery.Update_SessionOnCompleted, 1,
               Convert.ToString(DateTime.UtcNow.Ticks), connectionToken);
        }

        public void StartSession(SignalrRequest signalrRequest)
        {
            /// starts session for a particular browser client
            var jsonData = signalrRequest.ResponseBody;

            if (string.IsNullOrEmpty(jsonData)) return;

            var negotiateResponseObj = JsonConvert.DeserializeObject<NegotiateResponse>(jsonData);

            var sessionObj = new SessionDto
            {
                ConnectionId = negotiateResponseObj.ConnectionId,
                ConnectionToken = negotiateResponseObj.ConnectionToken,
                NegotiateData = jsonData,
                StartTimeStamp = Convert.ToString(DateTime.UtcNow.Ticks)
            };

            _sqlOperation.Execute(ExecuteSqlQuery.InsertRow_Session,
                sessionObj.ConnectionId, sessionObj.ConnectionToken, 0,
                sessionObj.StartTimeStamp, "0", sessionObj.NegotiateData);
        }
    }
}
