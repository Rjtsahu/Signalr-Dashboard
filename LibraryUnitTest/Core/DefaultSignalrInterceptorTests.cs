using LibraryUnitTest.TestEnables;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sahurjt.Signalr.Dashboard.Core;
using System;

namespace LibraryUnitTest.Core
{
    [TestClass]
    public class DefaultSignalrInterceptorTests
    {
        private Mock<IOwinContext> owinContextMock;
        private Mock<IOwinRequest> owinRequestMock;
        private Mock<IOwinResponse> owinResponseMock;
        private Mock<IDataTracing> tracingServiceMock;

        private DefaultSignalrInterceptor interceptorPreRequest;
        private DefaultSignalrInterceptor interceptorPostRequest;

        [TestInitialize]
        public void Init()
        {
            owinContextMock = new Mock<IOwinContext>();
            owinRequestMock = new Mock<IOwinRequest>();
            owinResponseMock = new Mock<IOwinResponse>();
            tracingServiceMock = new Mock<IDataTracing>();

            owinContextMock.Setup(s => s.Request).Returns(owinRequestMock.Object);
            owinContextMock.Setup(s => s.Response).Returns(owinResponseMock.Object);

            owinRequestMock.Setup(s => s.Uri).Returns(new Uri(Constants.ConnectUrl));

            SetupFakeDataTracing();
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPostRequest = new DefaultSignalrInterceptor(owinContextMock.Object, TimeSpan.FromSeconds(1));
        }

        [TestMethod]
        public void Test_OnPreRequest()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/start"));
            interceptorPreRequest.InvokeRequestMethod();
            tracingServiceMock.Verify(v => v.AddRequestTrace(It.IsAny<SignalrRequest>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_OnPostRequest()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/start"));
            interceptorPostRequest = new DefaultSignalrInterceptor(owinContextMock.Object, TimeSpan.FromSeconds(1));        
            interceptorPostRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/negotiate"));
            interceptorPostRequest = new DefaultSignalrInterceptor(owinContextMock.Object, TimeSpan.FromSeconds(1));
            interceptorPostRequest.InvokeRequestMethod();

            tracingServiceMock.Verify(v => v.CompleteRequestTrace(It.IsAny<SignalrRequest>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Test_PostNegotiate()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/negotiate"));
            interceptorPostRequest.InvokeRequestMethod();
            tracingServiceMock.Verify(v => v.StartSession(It.IsAny<SignalrRequest>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_OnAbort()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/abort"));
            interceptorPreRequest.InvokeRequestMethod();
            tracingServiceMock.Verify(v => v.FinishSession(It.IsAny<string>()), Times.Exactly(1));
        }

        [TestMethod]
        public void Test_LifeCycleMethod_OnPreRequest()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/connect"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/negotiate"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/ping"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/poll"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/reconnect"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/send"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/start"));
            interceptorPreRequest = new DefaultSignalrInterceptor(owinContextMock.Object);
            interceptorPreRequest.InvokeRequestMethod();

            tracingServiceMock.Verify(v => v.AddRequestTrace(It.IsAny<SignalrRequest>()), Times.Exactly(7));
        }

        private void SetupFakeDataTracing()
        {

            tracingServiceMock.Setup(s => s.AddRequestTrace(It.IsAny<SignalrRequest>()));
            tracingServiceMock.Setup(s => s.CompleteRequestTrace(It.IsAny<SignalrRequest>()));
            tracingServiceMock.Setup(s => s.FinishSession(It.IsAny<string>()));
            tracingServiceMock.Setup(s => s.StartSession(It.IsAny<SignalrRequest>()));

            DashboardGlobal.ServiceResolver.Replace<IDataTracing, DefaultDataTracing>(tracingServiceMock.Object);
        }
    }
}
