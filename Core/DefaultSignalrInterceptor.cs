using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Core.Message;
using Sahurjt.Signalr.Dashboard.Helpers;
using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultSignalrInterceptor : SignalrInterceptorBase
    {
        private readonly ISignalrInterceptorOperation _interceptorOperation;

        public DefaultSignalrInterceptor(IOwinContext owinContext) : base(owinContext) { }

        public DefaultSignalrInterceptor(IOwinContext owinContext, TimeSpan pipelineProcessingTime) : base(owinContext, pipelineProcessingTime) { }


        public override void OnAbort()
        {
            LogHelper.Log("OnAbort");
        }

        public override void OnConnect()
        {
            LogHelper.Log("OnConnect");
        }

        public override void OnNegotiate()
        {
            LogHelper.Log("OnNegotiate");

        }

        public override void OnPing()
        {
            LogHelper.Log("OnPing");

        }

        public override void OnPool()
        {
            LogHelper.Log("OnPool");
        }

        public override void OnPostRequest()
        {
            LogHelper.Log("OnPostRequest");
           // this.SignalrRequest.OwinContext.Response
            
            //       _interceptorOperation.StartTracing();
        }

        public override void OnPreRequest()
        {
            LogHelper.Log("OnPreRequest");
            //     _interceptorOperation.StartTracing();
        }

        public override void OnReconnect()
        {
            LogHelper.Log("OnReconnect");

        }

        public override void OnSend()
        {
            LogHelper.Log("OnSend");

        }

        public override void OnStart()
        {
            LogHelper.Log("OnStart");

        }
    }
}
