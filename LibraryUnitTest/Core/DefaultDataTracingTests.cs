using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using LibraryUnitTest.TestEnables;
using Microsoft.Owin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SignalrDashboard.Core;
using SignalrDashboard.DataStore;
using SignalrDashboard.DataStore.Dto;
using SignalrDashboard.Extensions;

namespace LibraryUnitTest.Core
{
    [TestClass]
    public class DefaultDataTracingTests
    {

        private Mock<ISqlOperation> sqlOperationMock;

        private Mock<IOwinContext> owinContextMock;
        private Mock<IOwinRequest> owinRequestMock;
        private Mock<IOwinResponse> owinResponseMock;

        private Mock<IPrincipal> principleMock;
        private Mock<IIdentity> identityMock;

        private IDataTracing dataTracingService;

        [TestInitialize]
        public void Init()
        {
            sqlOperationMock = new Mock<ISqlOperation>();
            dataTracingService = new DefaultDataTracing(sqlOperationMock.Object);

            owinRequestMock = new Mock<IOwinRequest>();
            owinResponseMock = new Mock<IOwinResponse>();
            owinContextMock = new Mock<IOwinContext>();

            principleMock = new Mock<IPrincipal>();
            identityMock = new Mock<IIdentity>();

            owinContextMock.Setup(s => s.Request).Returns(owinRequestMock.Object);
            owinContextMock.Setup(s => s.Response).Returns(owinResponseMock.Object);
        }

        [TestMethod]
        public void TestFinishSession_WhenSuccess()
        {
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            dataTracingService.FinishSession("fake_token");
            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(),
                It.IsAny<object[]>()), Times.Exactly(1));
        }


        [TestMethod]
        public void TestCompleteRequestTrace_WhenSuccess()
        {

            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream());

            var signalrRequest = new SignalrRequest(owinContextMock.Object);

            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            dataTracingService.CompleteRequestTrace(signalrRequest);
            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(),
                It.IsAny<object[]>()), Times.Exactly(1));
        }

        [TestMethod]
        public void TestStartSession_WhenSaved()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new SessionDto { SessionId = 1 });

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream(jsonBytes));

            var signalrRequest = new SignalrRequest(owinContextMock.Object);

            dataTracingService.StartSession(signalrRequest);

            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<string>(),
             It.IsAny<object[]>()), Times.Exactly(1));

            sqlOperationMock.Verify(v => v.Execute(It.IsAny<ExecuteSqlQuery>(),
            It.IsAny<object[]>()), Times.Exactly(2));

        }


        [TestMethod]
        public void TestStartSession_WhenSessionNotExists()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));

            SessionDto nullDto = null;
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(nullDto);

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream(jsonBytes));

            var signalrRequest = new SignalrRequest(owinContextMock.Object);

            dataTracingService.StartSession(signalrRequest);

            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<string>(),
             It.IsAny<object[]>()), Times.Exactly(1));

            sqlOperationMock.Verify(v => v.Execute(It.IsAny<ExecuteSqlQuery>(),
            It.IsAny<object[]>()), Times.Exactly(1));

        }


        [TestMethod]
        public void TestStartSession_WhenJsonEmpty()
        {
            var jsonBytes = Encoding.ASCII.GetBytes("");

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));

            SessionDto nullDto = null;
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(nullDto);

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream(jsonBytes));

            var signalrRequest = new SignalrRequest(owinContextMock.Object);

            dataTracingService.StartSession(signalrRequest);

            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<string>(),
             It.IsAny<object[]>()), Times.Exactly(0));

            sqlOperationMock.Verify(v => v.Execute(It.IsAny<ExecuteSqlQuery>(),
            It.IsAny<object[]>()), Times.Exactly(0));

        }

        [TestMethod]
        public void TestAddRequestTrace_WhenSuccess()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));

            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new SessionDto());

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());

            var signalrRequest = new SignalrRequest(owinContextMock.Object);
            var result = dataTracingService.AddRequestTrace(signalrRequest);

            Assert.IsTrue(result);
        }


        [TestMethod]
        public void TestAddRequestTrace_WhenSessionNotExists_Failure()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            SessionDto nullDto = null;
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(nullDto);

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());

            var signalrRequest = new SignalrRequest(owinContextMock.Object);
            var result = dataTracingService.AddRequestTrace(signalrRequest);

            Assert.IsFalse(result);
        }


        [TestMethod]
        public void TestStartSession_WhenNotSaved()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(0);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new SessionDto { SessionId = 1 });

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream(jsonBytes));

            var signalrRequest = new SignalrRequest(owinContextMock.Object);

            dataTracingService.StartSession(signalrRequest);

            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<string>(),
             It.IsAny<object[]>()), Times.Exactly(0));

            sqlOperationMock.Verify(v => v.Execute(It.IsAny<ExecuteSqlQuery>(),
            It.IsAny<object[]>()), Times.Exactly(1));

        }

        [TestMethod]
        public void TestAddRequestTrace_OnVariousCollationBranches()
        {
            var jsonBytes = Encoding.ASCII.GetBytes(Constants.NegotiateJsonResponse);

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            sqlOperationMock.Setup(s => s.Select<SessionDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new SessionDto { SessionId = 1 });

            owinRequestMock.Setup(s => s.Path).Returns(new PathString("/negotiate"));
            owinRequestMock.Setup(s => s.Uri).Returns(new Uri("http://fake.com"));
            owinRequestMock.SetReturnsDefault("");

            owinRequestMock.Setup(s => s.Body).Returns(new MemoryStream());
            owinResponseMock.Setup(s => s.Body).Returns(new MemoryStream(jsonBytes));

            owinRequestMock.Setup(s => s.LocalPort).Returns(8080);
            owinRequestMock.Setup(s => s.RemotePort).Returns(8000);
            // CASE 1
            owinRequestMock.Setup(s => s.User).Returns(principleMock.Object);

            var signalrRequest = new SignalrRequest(owinContextMock.Object);
            Assert.IsTrue(dataTracingService.AddRequestTrace(signalrRequest));

            // CASE 2
            principleMock.Setup(s => s.Identity).Returns(identityMock.Object);

            signalrRequest = new SignalrRequest(owinContextMock.Object);
            Assert.IsTrue(dataTracingService.AddRequestTrace(signalrRequest));

            // CASE 3
            principleMock.Setup(s => s.Identity.Name).Returns("");

            signalrRequest = new SignalrRequest(owinContextMock.Object);
            Assert.IsTrue(dataTracingService.AddRequestTrace(signalrRequest));
        }

        [TestMethod]
        public void Test_RequestReadBody_Failure()
        {
            var streamMock = new Mock<Stream>();
            streamMock.Setup(s => s.CanRead).Returns(false);

            owinRequestMock.Setup(s => s.Body).Returns(streamMock.Object);
            Assert.IsNull(owinRequestMock.Object.ReadBody());
        }

        [TestMethod]
        public void Test_ResponseReadBody_Failure()
        {
            var streamMock = new Mock<Stream>();
            streamMock.Setup(s => s.CanRead).Returns(false);

            owinResponseMock.Setup(s => s.Body).Returns(streamMock.Object);
            Assert.IsNull(owinResponseMock.Object.ReadBody());
        }
    }
}
