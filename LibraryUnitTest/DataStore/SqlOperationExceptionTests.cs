using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalrDashboard.DataStore;
using System;

namespace LibraryUnitTest.DataStore
{
    [TestClass]
    public class SqlOperationExceptionTests
    {
        [TestMethod]
        public void Test_Const()
        {
            var sqlExcep = new SqlOperationException();
            Assert.IsNotNull(sqlExcep);
            Assert.IsTrue(sqlExcep.ToString().Contains("Unknown database exception"));

        }

        [TestMethod]
        public void Test_InnerExeption()
        {
            var baseException = new Exception("base exception", new ArgumentException());

            var sqlExcep = new SqlOperationException("sql exception", baseException);

            Assert.IsNotNull(sqlExcep);
            Assert.IsTrue(sqlExcep.ToString().Contains("sql exception"));

        }
    }
}
