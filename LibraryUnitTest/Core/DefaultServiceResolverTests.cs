using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalrDashboard.Core;
using SignalrDashboard.DataStore.Dto;
using System;

namespace LibraryUnitTest.Core
{
    [TestClass]
    public class DefaultServiceResolverTests
    {
        private DefaultServiceResolver serviceResolver;

        [TestInitialize]
        public void Test_ConstructorWithDefaultServiceRegister()
        {
            serviceResolver = new DefaultServiceResolver();
        }

        [TestMethod]
        public void Test_GetService_WhenRegistered()
        {
            var service = serviceResolver.GetService<IDataTracing>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IDataTracing));
        }


        [TestMethod]
        public void Test_GetService_WhenNotRegistered()
        {
            var service = serviceResolver.GetService<IDataTableObject>();
            Assert.IsNull(service);
        }

        [TestMethod]
        public void Test_Register_Success()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>();
            var service = serviceResolver.GetService<IDataTableObject>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IDataTableObject));
        }


        [TestMethod]
        public void Test_Register_ShouldThrowException_WhenKeyIsNotInterface()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                serviceResolver.Register<SessionDto, SessionDto>()
            );
        }

        [TestMethod]
        public void Test_Register_ShouldThrowException_WhenAlreadyRegistered()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>();

            Assert.ThrowsException<Exception>(() =>
                serviceResolver.Register<IDataTableObject, SessionDto>()
            );
        }


        [TestMethod]
        public void Test_RegisterFunc_Success()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>(() => new SessionDto());
            var service = serviceResolver.GetService<IDataTableObject>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IDataTableObject));
        }


        [TestMethod]
        public void Test_RegisterFunc_ShouldThrowException_WhenKeyIsNotInterface()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                serviceResolver.Register<SessionDto, SessionDto>(() => new SessionDto())
            );
        }

        [TestMethod]
        public void Test_RegisterFunc_ShouldThrowException_WhenAlreadyRegistered()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>();

            Assert.ThrowsException<Exception>(() =>
                serviceResolver.Register<IDataTableObject, SessionDto>(() => new SessionDto())
            );
        }

        [TestMethod]
        public void Test_Replace_Success()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>(()=>new SessionDto());
            serviceResolver.Replace<IDataTableObject, SessionDto>(new SessionDto());
            var service = serviceResolver.GetService<IDataTableObject>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IDataTableObject));
        }

        [TestMethod]
        public void Test_Replace_ShouldThrowException_WhenKeyIsNotInterface()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                serviceResolver.Replace<SessionDto, SessionDto>(new SessionDto())
            );
        }

        [TestMethod]
        public void Test_Replace_ShouldThrowException_WhenAlreadyRegistered()
        {
            Assert.ThrowsException<Exception>(() =>
                serviceResolver.Replace<IDataTableObject, SessionDto>(new SessionDto())
            );
        }

        [TestMethod]
        public void Test_ReplaceFunc_Success()
        {
            serviceResolver.Register<IDataTableObject, SessionDto>(() => new SessionDto());
            serviceResolver.Replace<IDataTableObject, SessionDto>(() => new SessionDto());
            var service = serviceResolver.GetService<IDataTableObject>();
            Assert.IsNotNull(service);
            Assert.IsInstanceOfType(service, typeof(IDataTableObject));
        }


        [TestMethod]
        public void Test_ReplaceFunc_ShouldThrowException_WhenKeyIsNotInterface()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                serviceResolver.Replace<SessionDto, SessionDto>(() => new SessionDto())
            );
        }

        [TestMethod]
        public void Test_ReplaceFunc_ShouldThrowException_WhenAlreadyRegistered()
        {
            Assert.ThrowsException<Exception>(() =>
                serviceResolver.Replace<IDataTableObject, SessionDto>(() => new SessionDto())
            );
        }

        [TestMethod]
        public void Test_Dispose()
        {
            serviceResolver.Dispose();

            var service = serviceResolver.GetService<IDataTracing>();
            Assert.IsNull(service);
        }
    }
}
