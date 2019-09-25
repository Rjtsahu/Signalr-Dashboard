using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SignalrDashboard.Core;
using SignalrDashboard.DataStore;
using SignalrDashboard.DataStore.Dto;
using SignalrDashboard.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
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
            sqlOperationMock.Setup(s => s.Select<HubDataDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new HubDataDto { Id = 1 });
            sqlOperationMock.Setup(s => s.SelectMultiple<HubDataDto>(It.IsAny<SelectSqlQuery>(), It.IsAny<object[]>())).Returns(new List<HubDataDto> {
                new HubDataDto{ Id = 1} , new HubDataDto{ Id =2}
            });

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

        //// DTO extension tests

        [TestMethod]
        public void Test_AsBoolean()
        {
            var result = hubDataDto.AsBoolean("true");
            Assert.IsTrue(result);
            result = hubDataDto.AsBoolean("false");
            Assert.IsFalse(result);
            result = hubDataDto.AsBoolean("invalid_data");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Test_DateTimeConversion()
        {
            var date = DateTime.Now;
            var result = hubDataDto.AsDateTime(hubDataDto.AsEpoch(date));
            Assert.AreEqual(date.Date, result.Date);
            Assert.AreEqual(date.Day, result.Day);
            Assert.AreEqual(date.Hour, result.Hour);
            Assert.AreEqual(date.Minute, result.Minute);
            Assert.AreEqual(date.Second, result.Second);
        }

        [TestMethod]
        public void Test_CsvAsList()
        {
            var result = hubDataDto.CsvAsList(null);
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

            result = hubDataDto.CsvAsList("test,mock");
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);

        }

        [TestMethod]
        public void Test_GetAll_Success()
        {
            DashboardGlobal.ServiceResolver.Replace<ISqlOperation, ISqlOperation>(sqlOperationMock.Object);

            var result = hubDataDto.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.ToList().Count);
        }


        [TestMethod]
        public void Test_GetSingle_Success()
        {
            DashboardGlobal.ServiceResolver.Replace<ISqlOperation, ISqlOperation>(sqlOperationMock.Object);

            var result = hubDataDto.GetSingle(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void Test_GetAll_Failure()
        {
            var dummyDto = new DummyDto();

            DashboardGlobal.ServiceResolver.Replace<ISqlOperation, ISqlOperation>(sqlOperationMock.Object);

            Assert.ThrowsException<NotImplementedException>(
                () => dummyDto.GetAll()
             );
        }

        [TestMethod]
        public void Test_GetSingle_Failure()
        {
            var dummyDto = new DummyDto();

            DashboardGlobal.ServiceResolver.Replace<ISqlOperation, ISqlOperation>(sqlOperationMock.Object);

            Assert.ThrowsException<NotImplementedException>(
                () => dummyDto.GetSingle(1)
             );
            LogHelper.Log(dummyDto);
            LogHelper.SetLogging(false);
        }


        /// <summary>
        /// stub table to unit test extension method GetSingle,GetAll failure case.
        /// </summary>
        class DummyDto : IDataTableObject
        {
            public string TableName => "DummyTable";

            public bool Save(ISqlOperation sqlOperation)
            {
                throw new NotImplementedException();
            }

            public Task<bool> SaveAsync(ISqlOperation sqlOperation)
            {
                throw new NotImplementedException();
            }
        }
    }
}
