using System;
using System.Collections.Generic;

namespace IntrepidProducts.IocContainer.Strategy
{
    public abstract class StrategyAbstract : IIocContainer
    {
        public bool IgnoreDuplicateRegisterRequests { get; set; }

        public abstract void Register(Type fromType, Type concreteType);
        public abstract void Register(string key, Type fromType, Type concreteType);
        public abstract void Register(Type type);
        public abstract void Register(Type fromType, object obj);
        public abstract void Register(Type fromType, Type concreteType, bool useDefaultConstructor);
        public abstract void Register<I, T>() where T : I, new();
        public abstract void RegisterTransient(Type type);
        public abstract void RegisterTransient(Type fromType, Type concreteType);
        public abstract void RegisterTransient(string key, Type fromType, Type concreteType);

        public abstract void RegisterInstance<T>(T instance) where T : class;
        public abstract void RegisterInstance(Type fromType, object instance);
        public abstract void RegisterInstance<T>(string key, T instance) where T : class;

        public void Register<I, T>(string key) where T : I, new()
        {
            Register(key, typeof(I), typeof(T));
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