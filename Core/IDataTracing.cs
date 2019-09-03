
namespace Sahurjt.Signalr.Dashboard.Core
{
    internal interface IDataTracing
    {
        void StartSession(SignalrRequest signalrRequest);

        bool AddRequestTrace(string owinRequestId, SignalrRequest signalrRequest);

        bool CompleteRequestTrace(string owinRequestId, SignalrRequest signalrRequest);

        void FinishSession(string connectionToken);
    }

}
