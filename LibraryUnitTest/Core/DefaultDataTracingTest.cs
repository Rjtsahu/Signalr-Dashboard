using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sahurjt.Signalr.Dashboard.Core;
using Sahurjt.Signalr.Dashboard.DataStore;

namespace LibraryUnitTest.Core
{
    [TestClass]
    public class DefaultDataTracingTest
    {
        private Mock<ISqlOperation> sqlOperationMock;
        private IDataTracing dataTracingService;

        [TestInitialize]
        public void Init()
        {
            sqlOperationMock = new Mock<ISqlOperation>();
            dataTracingService = new DefaultDataTracing(sqlOperationMock.Object);
        }

        [TestMethod]
        public void TestFinishSessionSuccess()
        {
            sqlOperationMock.Setup(s => s.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(), It.IsAny<object[]>()));
            dataTracingService.FinishSession("fake_token");
            sqlOperationMock.Verify(v => v.ExecuteAsync(It.IsAny<ExecuteSqlQuery>(),
                It.IsAny<object[]>()), Times.Exactly(1));
        }

    }
}
