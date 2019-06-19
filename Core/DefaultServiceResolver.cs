using Sahurjt.Signalr.Dashboard.DataStore;
using System;
using System.Collections.Generic;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal class DefaultServiceResolver : IServiceResolver, IDisposable
    {
        private readonly Dictionary<Type, Func<object>> _resolver = new Dictionary<Type, Func<object>>();
        private static readonly string _databaseConnectionString = "Data Source=C:\\db\\sample.db;Version=3;New=True;";

        public DefaultServiceResolver()
        {
            RegisterDefaultServices();
        }

        private void RegisterDefaultServices()
        {
            Register<ISqlOperation, SqliteOperation>(() => new SqliteOperation(_databaseConnectionString));
        }

        public TInterface GetService<TInterface>()
        {
            if (_resolver.TryGetValue(typeof(TInterface), out var activator))
            {

                var obj = activator();
                return (TInterface)obj;
            }

            return default(TInterface);
        }

        public void Register<TInterface, TService>(Func<TService> activator) where TService : TInterface
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException("Service type must be an interface.");
            }

            if (_resolver.ContainsKey(typeof(TInterface)))
            {
                throw new Exception("The object intitalizer is already registered for this interface.");
            }

            _resolver.Add(typeof(TInterface), activator as Func<object>);
        }

        public void Replace<TInterface, TService>(Func<TService> activator) where TService : TInterface
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException("Service type must be an interface.");
            }

            if (!_resolver.ContainsKey(typeof(TInterface)))
            {
                throw new Exception("No class is registered for this interface.");
            }

            _resolver.Remove(typeof(TInterface));
            _resolver.Add(typeof(TInterface), activator as Func<object>);
        }

        public void Dispose()
        {
            _resolver.Clear();
        }
    }
}
