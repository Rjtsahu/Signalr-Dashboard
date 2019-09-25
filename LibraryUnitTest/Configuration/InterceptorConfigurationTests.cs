using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalrDashboard.Configuration;

namespace LibraryUnitTest.Configuration
{
    [TestClass()]
    public class InterceptorConfigurationTests
    {
        [TestMethod()]
        public void InterceptorConfigurationTest()
        {
            var config = new InterceptorConfiguration();

            Assert.IsNotNull(config);
            Assert.AreEqual("/dashboard", config.DefaultDashboardRoute);
            Assert.AreEqual("/signalr", config.DefaultSignalrRoute);
        }

    }
}