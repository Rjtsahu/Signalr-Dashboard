using Microsoft.Owin.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using SignalrDashboard.Extensions;

namespace LibraryUnitTest.Extensions
{
    [TestClass]
    public class OwinExtensionTests
    {
        [TestMethod]
        public void Test_UserSignalrDashboard()
        {
            IAppBuilder builder = new AppBuilder();

            builder.UseSignalrDashboard();
            var pipelineObj = builder.Build();
            Assert.IsNotNull(pipelineObj);
        }

    }
}
