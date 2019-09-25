using Microsoft.Owin;
using SignalrDashboard.Helpers;
using System;

namespace SignalrDashboard.Core
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


        protected override void OnAbort()
        {
            LogHelper.Log("OnAbort");

            _tracer.FinishSession(CurrentRequest.QueryCollection.ConnectionToken);
        }

        protected override void OnConnect()
        {
            LogHelper.Log("OnConnect");
        }

        protected override void OnNegotiate()
        {
            LogHelper.Log("OnNegotiate");
        }

        protected override void AfterNegotiate()
        {
            LogHelper.Log("AfterNegotiate");
            _tracer.StartSession(CurrentRequest);
        }


        protected override void OnPing()
        {
            LogHelper.Log("OnPing");

        }

        protected override void OnPoll()
        {
            LogHelper.Log("OnPool");
        }

        protected override void OnReconnect()
        {
            LogHelper.Log("OnReconnect");

        }

        protected override void OnSend()
        {
            LogHelper.Log("OnSend");

        }

        protected override void OnStart()
        {
            LogHelper.Log("OnStart");
        }

        protected override void OnPostRequest()
        {
            LogHelper.Log("OnPostRequest");
            _tracer.CompleteRequestTrace(CurrentRequest);

        }

        protected override void OnPreRequest()
        {
            LogHelper.Log("OnPreRequest");
            _tracer.AddRequestTrace(CurrentRequest);
        }
    }
}
