using System;
using System.Collections.Generic;
using IntrepidProducts.IocContainer;
using Microsoft.Extensions.DependencyInjection;

namespace IntrepidProducts.Ioc.MicrosoftStrategy
{
    public class MicrosoftStrategy : StrategyAbstract
    {
        private IServiceProvider _serviceProvider;
        private IServiceCollection _serviceCollection = new ServiceCollection();

        private MicrosoftStrategyKeyHelper _helper = new MicrosoftStrategyKeyHelper();

        private IServiceProvider ServiceProvider
        {
            get
            {
                return _serviceProvider ??= _serviceCollection.BuildServiceProvider();
            }
        }

        public override void InitContainer()
        {
            _serviceCollection = new ServiceCollection();
            _serviceProvider = null;
            _helper = new MicrosoftStrategyKeyHelper();
        }

        public override void RegisterTransient(Type abstractType, Type concreteType)
        {
            _serviceCollection.AddTransient(abstractType, concreteType);
        }

        public override void RegisterTransient(string key, Type abstractType, Type concreteType)
        {
            _serviceCollection.AddTransient(concreteType);
            _helper.Register(abstractType, concreteType, key);
        }

        public override void RegisterTransient(Type type)
        {
            RegisterTransient(type.ToString(), type, type);
        }

        public override void RegisterInstance(Type abstractType, Object instance)
        {
            _serviceCollection.AddSingleton(abstractType, instance);
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            _serviceCollection.AddSingleton(typeof(T), instance);
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            _helper.RegisterInstance<T>(key, instance); //Service Collection not used
        }

        public override void RegisterSingleton<I, T>()
        {
            _serviceCollection.AddSingleton(typeof(T));
        }

        public override void RegisterSingleton(Type type)
        {
            RegisterSingleton(type, type);
        }

        public override void RegisterSingleton(Type abstractType, Type concreteType)
        {
            _serviceCollection.AddSingleton(abstractType, concreteType);
        }
        public override void RegisterSingleton(string key, Type abstractType, Type concreteType)
        {
            _serviceCollection.AddSingleton(concreteType);
            _helper.Register(abstractType, concreteType, key);
        }

        public override T Resolve<T>()
        {
            var resolvedObject =  ServiceProvider.GetService(typeof(T));

            if (resolvedObject == null)
            {
                throw new NotRegisteredException(typeof(T));
            }

            return (T)resolvedObject;
        }

        public override T Resolve<T>(string key)
        {
            return _helper.Resolve<T>(ServiceProvider, key);
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            return _helper.ResolveAll<T>(ServiceProvider);
        }

        public override void Release(object component)
        {
            _serviceProvider = null;
            _serviceCollection = new ServiceCollection();
            _helper = new MicrosoftStrategyKeyHelper();
        }

        public override bool IsRegistered<T>()
        {
            try
            {
                Resolve<T>();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override bool IsRegistered<T>(string key)
        {
            try
            {
                Resolve<T>(key);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}