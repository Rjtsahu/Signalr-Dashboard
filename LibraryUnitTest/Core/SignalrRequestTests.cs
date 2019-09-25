using LibraryUnitTest.TestEnables;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SignalrDashboard.Core;
using System;

namespace LibraryUnitTest.Core
{
    [TestClass]
    public class SignalrRequestTests
    {
        private Mock<IOwinContext> owinContextMock;
        private Mock<IOwinRequest> owinRequestMock;
        private Mock<IOwinResponse> owinResponseMock;

        private SignalrRequest signalrRequest;

        [TestInitialize]
        public void Init()
        {
            owinContextMock = new Mock<IOwinContext>();
            owinRequestMock = new Mock<IOwinRequest>();
            owinResponseMock = new Mock<IOwinResponse>();

            owinContextMock.Setup(s => s.Request).Returns(owinRequestMock.Object);
            owinContextMock.Setup(s => s.Response).Returns(owinResponseMock.Object);

            owinRequestMock.Setup(s => s.Uri).Returns(new Uri(Constants.ConnectUrl));
        }

        [TestMethod]
        public void Test_Constructor()
        {
            signalrRequest = new SignalrRequest(owinContextMock.Object);

            Assert.IsNotNull(signalrRequest);
            Assert.IsNotNull(signalrRequest.OwinContext);
            Assert.IsNull(signalrRequest.OwinRequestId);
        }

        [TestMethod]
        public void Test_GetRequestTypeValid()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/negotiate"));

            signalrRequest = new SignalrRequest(owinContextMock.Object);

            Assert.IsTrue(RequestType.None != signalrRequest.Type);
        }


        [TestMethod]
        public void Test_GetRequestTypeNotValid()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/unknown"));

            signalrRequest = new SignalrRequest(owinContextMock.Object);

            Assert.IsFalse(RequestType.None != signalrRequest.Type);
        }

        [TestMethod]
        public void Test_GetRequestTypeNotValid_LastSegmentis()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/unknown"));

            signalrRequest = new SignalrRequest(owinContextMock.Object);

            Assert.IsFalse(RequestType.None != signalrRequest.Type);
        }

        [TestMethod]
        public void Test_GetRequestTypeValid_WhenTwiceGet()
        {
            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/signalr/negotiate"));

            signalrRequest = new SignalrRequest(owinContextMock.Object);

            Assert.IsTrue(RequestType.None != signalrRequest.Type);

            Assert.IsTrue(RequestType.None != signalrRequest.Type);
        }

    }
}
