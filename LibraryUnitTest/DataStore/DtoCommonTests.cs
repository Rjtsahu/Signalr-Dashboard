using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sahurjt.Signalr.Dashboard.DataStore;
using Sahurjt.Signalr.Dashboard.DataStore.Dto;
using System.Threading.Tasks;

namespace LibraryUnitTest.DataStore
{
    [TestClass]
    public class DtoCommonTests
    {
        private Mock<ISqlOperation> sqlOperationMock;
        private Mock<ISqlOperation> sqlOperationFailureMock;

        private HubDataDto hubDataDto;
        private RequestDto requestDto;
        private SessionDto sessionDto;
        private SessionReportDto sessionReportDto;

        [TestInitialize]
        public void Init()
        {
            sqlOperationMock = new Mock<ISqlOperation>();

            sqlOperationMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(1);
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(Task.FromResult(1));

            sqlOperationFailureMock = new Mock<ISqlOperation>();

            sqlOperationFailureMock.Setup(s => s.Execute(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(0);
            sqlOperationFailureMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>())).Returns(Task.FromResult(0));

            hubDataDto = new HubDataDto();
            requestDto = new RequestDto();
            sessionDto = new SessionDto();
            sessionReportDto = new SessionReportDto();
        }


        [TestMethod]
        public void Test_TableNames()
        {
            Assert.AreEqual("HubData", hubDataDto.TableName);
            Assert.AreEqual("Session", sessionDto.TableName);
            Assert.AreEqual("Request", requestDto.TableName);
            Assert.AreEqual("SessionReport", sessionReportDto.TableName);
        }

        [TestMethod]
        public void Test_Dto_WHenSave_Succeeds()
        {
            Assert.IsTrue(hubDataDto.Save(sqlOperationMock.Object));
            Assert.IsTrue(hubDataDto.SaveAsync(sqlOperationMock.Object).Result);

            Assert.IsTrue(sessionDto.Save(sqlOperationMock.Object));
            Assert.IsTrue(sessionDto.SaveAsync(sqlOperationMock.Object).Result);

            Assert.IsTrue(requestDto.Save(sqlOperationMock.Object));
            Assert.IsTrue(requestDto.SaveAsync(sqlOperationMock.Object).Result);

            Assert.IsTrue(sessionReportDto.Save(sqlOperationMock.Object));
            Assert.IsTrue(sessionReportDto.SaveAsync(sqlOperationMock.Object).Result);
        }


        [TestMethod]
        public void Test_Dto_WHenSave_Fails()
        {
            Assert.IsFalse(hubDataDto.Save(sqlOperationFailureMock.Object));
            Assert.IsFalse(hubDataDto.SaveAsync(sqlOperationFailureMock.Object).Result);

            Assert.IsFalse(sessionDto.Save(sqlOperationFailureMock.Object));
            Assert.IsFalse(sessionDto.SaveAsync(sqlOperationFailureMock.Object).Result);

            Assert.IsFalse(requestDto.Save(sqlOperationFailureMock.Object));
            Assert.IsFalse(requestDto.SaveAsync(sqlOperationFailureMock.Object).Result);

            Assert.IsFalse(sessionReportDto.Save(sqlOperationFailureMock.Object));
            Assert.IsFalse(sessionReportDto.SaveAsync(sqlOperationFailureMock.Object).Result);
        }
    }
}
