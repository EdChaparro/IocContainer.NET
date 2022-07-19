using System;
using System.Collections.Generic;

namespace IntrepidProducts.IocContainer.Strategy
{
    public abstract class StrategyAbstract : IIocContainer
    {
        public bool IgnoreDuplicateRegisterRequests { get; set; }

        public abstract void RegisterSingleton(Type abstractType, Type concreteType);
        public abstract void RegisterSingleton(string key, Type abstractType, Type concreteType);
        public abstract void RegisterSingleton(Type type);
        public abstract void RegisterSingleton<I, T>() where T : I, new();

        public abstract void RegisterTransient(Type type);
        public abstract void RegisterTransient(Type abstractType, Type concreteType);
        public abstract void RegisterTransient(string key, Type abstractType, Type concreteType);

        public abstract void RegisterInstance(Type abstractType, object instance);
        public abstract void RegisterInstance<T>(T instance) where T : class;
        public abstract void RegisterInstance<T>(string key, T instance) where T : class;

        public void RegisterSingleton<I, T>(string key) where T : I, new()
        {
            RegisterSingleton(key, typeof(I), typeof(T));
        }

        public abstract T Resolve<T>();
        public abstract T Resolve<T>(string key);
        public abstract object Resolve(Type type);

        public abstract IEnumerable<T> ResolveAll<T>();

        public abstract void Release(object component);
        public abstract void InitContainer();

        public abstract bool IsRegistered<T>();
        public abstract bool IsRegistered(Type type);

        public abstract bool IsRegistered<T>(string key);
    }
}