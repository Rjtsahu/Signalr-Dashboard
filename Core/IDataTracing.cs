
namespace SignalrDashboard.Core
{
    internal interface IDataTracing
    {
        void StartSession(SignalrRequest signalrRequest);

        bool AddRequestTrace(SignalrRequest signalrRequest);

        void CompleteRequestTrace(SignalrRequest signalrRequest);

        void FinishSession(string connectionToken);

    }

}
