using LibraryUnitTest.TestEnables;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sahurjt.Signalr.Dashboard.Core.Message;
using System;

namespace LibraryUnitTest.Core.Message
{
    [TestClass]
    public class RequestQueryCollectionTests
    {

        [TestMethod]
        public void Test_Class()
        {
            var collection = new RequestQueryCollection(new Uri(Constants.ConnectUrl));

            Assert.IsNotNull(collection);
            Assert.IsTrue(!string.IsNullOrEmpty(collection.ClientProtocol));
            Assert.IsTrue(!string.IsNullOrEmpty(collection.ConnectionData));
            Assert.IsTrue(!string.IsNullOrEmpty(collection.Transport));
            Assert.IsTrue(!string.IsNullOrEmpty(collection.ConnectionToken));

            Assert.IsNotNull(collection.ConnectionDatas);
        }


        [TestMethod]
        public void Test_Class_WhenQueryStringIsNull()
        {
            var urlString = Constants.ConnectUrl.Split('?')[0];
            var collection = new RequestQueryCollection(new Uri(urlString));

            Assert.IsNotNull(collection);
            Assert.IsFalse(!string.IsNullOrEmpty(collection.ClientProtocol));
        }
    }
}
