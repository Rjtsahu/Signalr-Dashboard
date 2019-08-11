
namespace Sahurjt.Signalr.Dashboard.Core
{
    internal interface ISignalrInterceptorOperation
    {
        bool StartTracing();

        void AddTrace(SignalrRequest request);

        bool StopTracing();
    }

}
