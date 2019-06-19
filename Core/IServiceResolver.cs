using System;

namespace Sahurjt.Signalr.Dashboard.Core
{
    internal interface IServiceResolver
    {
        TInterface GetService<TInterface>();

        void Register(Type serviceType, Func<object> activator);
    }
}
