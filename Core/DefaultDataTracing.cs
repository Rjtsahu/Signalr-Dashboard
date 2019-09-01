using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultDataTracing : IDataTracing
    {
        public bool AddRequestTrace(string owinRequestId, SignalrRequest signalrRequest)
        {
            throw new NotImplementedException();
        }

        public bool CompleteRequestTrace(string owinRequestId, SignalrRequest signalrRequest)
        {
            throw new NotImplementedException();
        }

        public void FinishSession(string connectionId)
        {
            throw new NotImplementedException();
        }

        public void StartSession(SignalrRequest signalrRequest)
        {
            throw new NotImplementedException();
        }
    }
}
