using Microsoft.Owin;
using Sahurjt.Signalr.Dashboard.Helpers;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultSignalrInterceptor : SignalrInterceptorBase
    {
        public DefaultSignalrInterceptor(IOwinContext owinContext) : base(owinContext)
        {
        }
        public DefaultSignalrInterceptor(IOwinContext owinContext, bool pipelineProcessed) : base(owinContext, pipelineProcessed)
        {
        }

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
        }

        public override void OnPreRequest()
        {
            LogHelper.Log("OnPreRequest");
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
