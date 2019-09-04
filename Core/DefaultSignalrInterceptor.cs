using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Helpers;
using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultSignalrInterceptor : SignalrInterceptorBase
    {
        private readonly IDataTracing _tracer;

        public DefaultSignalrInterceptor(IOwinContext owinContext) : base(owinContext)
        {
            _tracer = DashboardGlobal.ServiceResolver.GetService<IDataTracing>();
        }

        public DefaultSignalrInterceptor(IOwinContext owinContext, TimeSpan pipelineProcessingTime) : base(owinContext, pipelineProcessingTime)
        {
            _tracer = DashboardGlobal.ServiceResolver.GetService<IDataTracing>();
        }


        public override void OnAbort()
        {
            LogHelper.Log("OnAbort");

            _tracer.FinishSession(CurrentRequest.QueryCollection.ConnectionToken);
        }

        public override void OnConnect()
        {
            LogHelper.Log("OnConnect");
        }

        public override void OnNegotiate()
        {
            LogHelper.Log("OnNegotiate");
        }

        public override void AfterNegotiate()
        {
            LogHelper.Log("AfterNegotiate");
            _tracer.StartSession(CurrentRequest);
        }


        public override void OnPing()
        {
            LogHelper.Log("OnPing");

        }

        public override void OnPool()
        {
            LogHelper.Log("OnPool");
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
        
        public override void OnPostRequest()
        {
            LogHelper.Log("OnPostRequest");
            _tracer.CompleteRequestTrace(CurrentRequest);
        }

        public override void OnPreRequest()
        {
            LogHelper.Log("OnPreRequest");
            _tracer.AddRequestTrace(CurrentRequest);
        }
    }
}
