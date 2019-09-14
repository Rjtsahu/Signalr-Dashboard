using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Configuration;
using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal abstract class SignalrInterceptorBase
    {
        protected readonly SignalrRequest CurrentRequest;

        protected bool PipelineProcessed { get; private set; }

        protected TimeSpan PipelineProcessingTime { get; private set; }

        protected string ConnectionId { get; set; }

        protected SignalrInterceptorBase(IOwinContext owinContext) : this(owinContext, TimeSpan.Zero) { }


        protected SignalrInterceptorBase(IOwinContext owinContext, TimeSpan pipelineProcessingTime)
        {

            CurrentRequest = new SignalrRequest(owinContext);
            PipelineProcessingTime = pipelineProcessingTime;
            PipelineProcessed = pipelineProcessingTime != TimeSpan.Zero;
        }

        public void InvokeRequestMethod()
        {
            if (PipelineProcessed)
            {
                if (CurrentRequest.Type == RequestType.Negotiate) {
                    AfterNegotiate();
                }
                OnPostRequest();
                return;
            }

            var requestType = CurrentRequest.Type;
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
                    OnPoll();
                    break;
                case RequestType.Ping:
                    OnPing();
                    break;
            }
            OnPreRequest();
        }

        protected abstract void OnPreRequest();
        protected abstract void OnPostRequest();

        protected abstract void OnNegotiate();
        protected abstract void AfterNegotiate();
        
        protected abstract void OnConnect();
        protected abstract void OnStart();
        
        protected abstract void OnAbort();
        protected abstract void OnReconnect();
        
        protected abstract void OnSend();
        protected abstract void OnPoll();
        protected abstract void OnPing();
    }
}
