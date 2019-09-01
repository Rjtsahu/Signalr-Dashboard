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

        public DefaultDataTracing()
        {
            _sqlOperation = DashboardGlobal.ServiceResolver.GetService<ISqlOperation>();
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

        public void FinishSession(string connectionId)
        {
            /// TODO ; IMPL
        }

        public void StartSession(SignalrRequest signalrRequest)
        {
            /// starts session for a particular browser client
            var jsonData = signalrRequest.OwinContext.Response.ReadBody();
            var negotiateResponseObj = JsonConvert.DeserializeObject<NegotiateResponse>(jsonData);

            var sessionObj = new SessionDto
            {
                ConnectionId = negotiateResponseObj.ConnectionId,
                ConnectionToken = signalrRequest.QueryCollection.ConnectionToken,
                NegotiateData = jsonData,
                StartTimeStamp = Convert.ToString(DateTime.UtcNow.Ticks)
            };

            _sqlOperation.Execute(ExecuteSqlQuery.InsertRow_Session,
                sessionObj.ConnectionId, sessionObj.ConnectionToken, 0,
                sessionObj.StartTimeStamp, "0", sessionObj.NegotiateData);
        }
    }
}
