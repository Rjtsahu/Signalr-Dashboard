using LibraryUnitTest.TestEnables;
using Microsoft.Owin.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using Sahurjt.Signalr.Dashboard.Middleware;
using System.Collections.Generic;
using System.IO;

namespace LibraryUnitTest.Middleware
{
    [TestClass]
    public class SignalrInterceptorMiddlewareTests
    {

        [TestMethod]
        public void Test_SignalrMiddlewarePipeline()
        {
            IAppBuilder builder = new AppBuilder();
            builder.Use<SignalrInterceptorMiddleware>("/signalr");
            var pipelineObj = builder.Build();
            bool passed;
            try
            {
                pipelineObj.Invoke(GetEnvironmentData());
                passed = true;
            }
            catch
            {
                passed = false;
            }
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void Test_NonSignalrMiddlewarePipeline()
        {
            IAppBuilder builder = new AppBuilder();
            builder.Use<SignalrInterceptorMiddleware>("/signalr");
            var pipelineObj = builder.Build();
            bool passed;
            try
            {
                var dictionary = GetEnvironmentData();
                dictionary[OwinConstants.RequestPath] = "/api";
                pipelineObj.Invoke(dictionary);
                passed = true;
            }
            catch
            {
                passed = false;
            }
            Assert.IsTrue(passed);
        }

        [TestMethod]
        public void Test_DashboaardMiddleware()
        {

            IAppBuilder builder = new AppBuilder();
            builder.Use<SignalrDashboardMiddleware>("/dashboard");
            var pipelineObj = builder.Build();
            bool passed;
            try
            {
                pipelineObj.Invoke(GetEnvironmentData());
                passed = true;
            }
            catch
            {
                passed = false;
            }
            Assert.IsTrue(passed);
        }

        private IDictionary<string, object> GetEnvironmentData()
        {
            var dictionary = new Dictionary<string, object>
            {
                { OwinConstants.RequestBody, new MemoryStream() },
                { OwinConstants.ResponseBody, new MemoryStream() },

                { OwinConstants.RequestScheme, "http" },
                { OwinConstants.RequestPathBase,"" },
                { OwinConstants.RequestPath, "/signalr/signalr" },
                { OwinConstants.RequestQueryString, "?transport=webSockets" },

                { OwinConstants.RequestHeaders,new Dictionary<string,string[]>{
                    { "host",new string[]{ "localhost"} }
                } }
            };

            return dictionary;
        }
    }
}
