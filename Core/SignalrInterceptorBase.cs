using Microsoft.Owin;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal abstract class SignalrInterceptorBase
    {
        protected readonly SignalrRequest SignalrRequest;
        protected bool PipelineProcessed { get; private set; }

        protected string ConnectionId { get; set; }

        protected SignalrInterceptorBase(IOwinContext owinContext) : this(owinContext, false)
        {
        }

        protected SignalrInterceptorBase(IOwinContext owinContext, bool pipelineProcessed)
        {

            SignalrRequest = new SignalrRequest(owinContext);
            PipelineProcessed = pipelineProcessed;
        }

        public void InvokeRequestMethod(bool pipelineProcessed = false)
        {
            PipelineProcessed = pipelineProcessed;
            if (PipelineProcessed)
            {
                OnPostRequest();
                return;
            }

            var requestType = SignalrRequest.Type;
            switch (requestType)
            {
                case RequestType.Negotiate:
                    OnNegotiate();
                    break;
                case RequestType.Connect:
                    OnConnect();
                    break;
                case RequestType.Start:
                    OnStart();
                    break;
                case RequestType.Abort:
                    OnAbort();
                    break;
                case RequestType.Reconnect:
                    OnReconnect();
                    break;
                case RequestType.Send:
                    OnSend();
                    break;
                case RequestType.Poll:
                    OnPool();
                    break;
                case RequestType.Ping:
                    OnPing();
                    break;
            }
        }

        public abstract void OnPreRequest();
        public abstract void OnPostRequest();

        public abstract void OnNegotiate();

        public abstract void OnConnect();
        public abstract void OnStart();

        public abstract void OnAbort();
        public abstract void OnReconnect();

        public abstract void OnSend();
        public abstract void OnPool();
        public abstract void OnPing();
    }
}
