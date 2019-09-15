using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sahurjt.Signalr.Dashboard.DataStore;
using System.Reflection;

namespace LibraryUnitTest.DataStore
{
    [TestClass]
    public class SqliteOperationTests
    {
        private static readonly string _connectionString = "Data Source=:memory:;Version=3;New=True;";

        private SqliteOperation sqliteOperationObject;

        [TestMethod]
        public void Test_VariousScenarios()
        {
            sqliteOperationObject = new SqliteOperation(_connectionString);

            Assert.AreEqual("Sqlite3", sqliteOperationObject.ProviderName);

            var dbCommand = sqliteOperationObject.GetType().InvokeMember("GetCommandParameter", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, sqliteOperationObject, new object[] { "SELECT * FROM Dummy" });
            Assert.IsNotNull(dbCommand);

            dbCommand = sqliteOperationObject.GetType().InvokeMember("GetCommandParameter", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, sqliteOperationObject, new object[] { "SELECT * FROM Dummy WHERE id = @id", 1 });
            Assert.IsNotNull(dbCommand);

            try
            {
                dbCommand = sqliteOperationObject.GetType().InvokeMember("GetCommandParameter", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, sqliteOperationObject, new object[] { "SELECT * FROM Dummy WHERE id = @id" });
            }
            catch
            {
                Assert.IsTrue(true);
            }

            try
            {
                sqliteOperationObject.GetType().InvokeMember("GetDbConnection", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, sqliteOperationObject, new object[] { });
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }

    }
}
