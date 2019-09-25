using System;

namespace SignalrDashboard.Core
{
    public interface IServiceResolver
    {
        TInterface GetService<TInterface>();

        void Register<TInterface, TService>() where TService : TInterface;

        void Register<TInterface, TService>(Func<TService> activator) where TService : TInterface;

        void Replace<TInterface, TService>(TInterface newService) where TService : TInterface;

        void Replace<TInterface, TService>(Func<TService> activator) where TService : TInterface;
    }
}
