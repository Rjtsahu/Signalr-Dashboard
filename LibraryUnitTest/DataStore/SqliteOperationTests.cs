using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Sahurjt.Signalr.Dashboard.DataStore;
using Sahurjt.Signalr.Dashboard.DataStore.Dto;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace LibraryUnitTest.DataStore
{
    [TestClass]
    public class SqliteOperationTests
    {
        private static readonly string _rawExecQuery = "UPDATE FAKE SET isFake = 1 WHERE id = 1;";
        private static readonly string _connectionToken = "abc123z";

        private Mock<DbCommand> dbCommandMock;
        private Mock<DbConnection> dbConnectionMock;
        private Mock<DbDataReader> dbDataReaderMock;

        private Mock<BaseSqlOperation> sqlOperationService;

        private ISqlQueryProvider sqlQueryProvider;

        [TestInitialize]
        public void Init()
        {
            dbCommandMock = new Mock<DbCommand>();
            dbConnectionMock = new Mock<DbConnection>();
            dbDataReaderMock = new Mock<DbDataReader>();
            sqlQueryProvider = new SqliteQueryProvider();

            sqlOperationService = new Mock<BaseSqlOperation>(sqlQueryProvider);

            sqlOperationService.Protected().Setup<DbCommand>("GetCommandParameter", ItExpr.IsAny<string>(), ItExpr.IsAny<object[]>()).Returns(dbCommandMock.Object);
            sqlOperationService.Protected().Setup<DbConnection>("GetDbConnection").Returns(dbConnectionMock.Object);
        }

        [TestMethod]
        public void Test_ExecuteSuccess()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Returns(1);

            var result = sqlOperationService.Object.Execute(ExecuteSqlQuery.Create_DatabaseTables);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_ExecuteException()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Throws(new System.Exception("failed to execute"));

            Assert.ThrowsException<SqlOperationException>(
                () => sqlOperationService.Object.Execute(ExecuteSqlQuery.Create_DatabaseTables)
             );
        }

        [TestMethod]
        public void Test_ExecuteAsyncSuccess()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Returns(1);

            var result = sqlOperationService.Object.ExecuteAsync(ExecuteSqlQuery.Create_DatabaseTables);

            Assert.AreEqual(1, result.Result);
        }

        [TestMethod]
        public async Task Test_ExecuteAsyncException()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Throws(new System.Exception("failed to execute"));

            await Assert.ThrowsExceptionAsync<SqlOperationException>(
                 async () => await sqlOperationService.Object.ExecuteAsync(ExecuteSqlQuery.Create_DatabaseTables)
              );
        }


        [TestMethod]
        public void Test_ExecuteRawSuccess()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Returns(1);

            var result = sqlOperationService.Object.Execute(_rawExecQuery);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Test_ExecuteRawException()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Throws(new System.Exception("failed to execute"));

            Assert.ThrowsException<SqlOperationException>(
                () => sqlOperationService.Object.Execute(_rawExecQuery)
             );
        }

        [TestMethod]
        public void Test_ExecuteAsyncRawSuccess()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Returns(1);

            var result = sqlOperationService.Object.ExecuteAsync(_rawExecQuery);

            Assert.AreEqual(1, result.Result);
        }

        [TestMethod]
        public async Task Test_ExecuteAsyncRawException()
        {
            dbCommandMock.Setup(s => s.ExecuteNonQuery()).Throws(new System.Exception("failed to execute"));

            await Assert.ThrowsExceptionAsync<SqlOperationException>(
                 async () => await sqlOperationService.Object.ExecuteAsync(_rawExecQuery)
              );
        }

        [TestMethod]
        public void Test_SelectSuccess()
        {
            dbDataReaderMock.SetupSequence(s => s.Read()).Returns(true).Returns(false);
            dbDataReaderMock.Setup(s => s["SessionId"]).Returns(1);
            dbDataReaderMock.Setup(s => s["ConnectionId"]).Returns("fake_connectionId");
            dbDataReaderMock.Setup(s => s["ConnectionToken"]).Returns(_connectionToken);

            //   dbCommandMock.Setup(s => s.ExecuteReader()).Returns(dbDataReaderMock.Object);

            //  var result = sqlOperationService.Object.Select<SessionDto>(SelectSqlQuery.GetSingle_Session_By_ConnectionToken, _connectionToken);
        }
    }
}
