using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal abstract class SignalrInterceptorBase
    {
        protected readonly SignalrRequest SignalrRequest;

        protected bool PipelineProcessed { get; private set; }

        protected TimeSpan PipelineProcessingTime { get; private set; }

        protected string ConnectionId { get; set; }

        protected InterceptorConfiguration Configuration { get { return DashboardGlobal.Configuration; } }

        protected SignalrInterceptorBase(IOwinContext owinContext) : this(owinContext, TimeSpan.Zero) { }


        protected SignalrInterceptorBase(IOwinContext owinContext, TimeSpan pipelineProcessingTime)
        {

            SignalrRequest = new SignalrRequest(owinContext);
            PipelineProcessingTime = pipelineProcessingTime;
            PipelineProcessed = pipelineProcessingTime != TimeSpan.Zero;
        }

        public void InvokeRequestMethod()
        {
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
