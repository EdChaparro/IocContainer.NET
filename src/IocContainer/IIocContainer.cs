using System;
using System.Collections.Generic;

namespace IntrepidProducts.IocContainer
{
    public interface IIocContainer
    {
        void Register(Type fromType, Type concreteType);
        void Register(string key, Type fromType, Type concreteType);
        void Register(Type type);
        void Register(Type fromType, Object obj);
        void Register(Type fromType, Type concreteType, bool useDefaultConstructor);

        void Register<I, T>() where T : I, new();
        void Register<I, T>(string key) where T : I, new();

        void RegisterInstance<T>(T instance) where T : class;
        void RegisterInstance<T>(string key, T instance) where T : class;

        void RegisterTransient(Type type);

        void RegisterTransient(Type fromType, Type concreteType);

        void RegisterTransient(string key, Type fromType, Type concreteType);

        bool IsRegistered<T>();
        bool IsRegistered<T>(string key);

        T Resolve<T>();
        T Resolve<T>(string key);

        IEnumerable<T> ResolveAll<T>();

        void InitContainer();
    }
}