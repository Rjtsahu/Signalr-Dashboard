using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahurjt.Signalr.Dashboard.Core
{
    class DefaultServiceResolver : IServiceResolver
    {
        private readonly Dictionary<Type, Func<object>> _resolver = new Dictionary<Type, Func<object>>();

        public TInterface GetService<TInterface>()
        {
            if (_resolver.TryGetValue(typeof(TInterface),out var activator)) {

                var obj =  activator();
                return (TInterface) obj;
            }
           
                throw new NotImplementedException();
        }

        public void Register(Type serviceType, Func<object> activator)
        {
            if (!serviceType.IsInterface)
            {
                throw new ArgumentException("Service type must be an interface.");
            }

            if (_resolver.ContainsKey(serviceType))
            {
                throw new Exception("The object intitalizer is already registered for this interface.");
            }

            _resolver.Add(serviceType, activator);
        }
    }
}
