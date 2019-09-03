using Sahurjt.Signalr.Dashboard.DataStore;
using System;
using System.Collections.Generic;

namespace Sahurjt.Signalr.Dashboard.Core
{
    /// <summary>
    /// Service resolver having singleton instances of dependecies.
    /// </summary>
    internal class DefaultServiceResolver : IServiceResolver, IDisposable
    {
        private readonly Dictionary<Type, object> _resolver = new Dictionary<Type, object>();


        public DefaultServiceResolver()
        {
            RegisterDefaultServices();
        }

        private void RegisterDefaultServices()
        {
            Register<ISqlQueryProvider, SqliteQueryProvider>();

            Register<ISqlOperation, SqliteOperation>(() => new SqliteOperation(DashboardGlobal.Configuration.ConnectionString));

            Register<IDataTracing, DefaultDataTracing>(() => new DefaultDataTracing(GetService<ISqlOperation>()));
        }

        public TInterface GetService<TInterface>()
        {
            if (_resolver.TryGetValue(typeof(TInterface), out var obj))
            {
                return (TInterface)obj;
            }

            return default(TInterface);
        }

        public void Register<TInterface, TService>() where TService : TInterface
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException("Service type must be an interface.");
            }

            if (_resolver.ContainsKey(typeof(TInterface)))
            {
                throw new Exception("The object intitalizer is already registered for this interface.");
            }
            _resolver.Add(typeof(TInterface), Activator.CreateInstance<TService>());
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

            _resolver.Add(typeof(TInterface), activator());
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
            _resolver.Add(typeof(TInterface), activator());
        }

        public void Dispose()
        {
            _resolver.Clear();
        }
    }
}
