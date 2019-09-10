using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sahurjt.Signalr.Dashboard.Configuration.Tests
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