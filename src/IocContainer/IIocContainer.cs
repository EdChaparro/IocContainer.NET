using System;
using System.Collections.Generic;

namespace IntrepidProducts.IocContainer
{
    public interface IIocContainer
    {
        void RegisterSingleton(Type abstractType, Type concreteType);
        void RegisterSingleton(string key, Type abstractType, Type concreteType);
        void RegisterSingleton(Type type);
        void RegisterSingleton<I, T>() where T : I, new();
        void RegisterSingleton<I, T>(string key) where T : I, new();

        void RegisterInstance(Type abstractType, Object instance);

        void RegisterInstance<T>(T instance) where T : class;
        void RegisterInstance<T>(string key, T instance) where T : class;


        void RegisterTransient(Type type);

        void RegisterTransient(Type abstractType, Type concreteType);

        void RegisterTransient(string key, Type abstractType, Type concreteType);

        bool IsRegistered<T>();
        bool IsRegistered<T>(string key);

        T Resolve<T>();
        T Resolve<T>(string key);

        IEnumerable<T> ResolveAll<T>();

        void InitContainer();
    }
}