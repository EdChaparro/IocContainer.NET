using Autofac;
using Autofac.Core.Registration;
using IntrepidProducts.IocContainer;
using System;
using System.Collections.Generic;

namespace IntrepidProducts.IoC.AutofacStrategy
{
    public class AutofacStrategy : StrategyAbstract
    {
        public AutofacStrategy(ContainerBuilder? containerBuilder = null)
        {
            _builder = containerBuilder;
        }

        private ContainerBuilder? _builder;

        private ContainerBuilder Builder
        {
            get { return _builder ??= new ContainerBuilder(); }
        }

        private ILifetimeScope? _scope;
        private ILifetimeScope Scope
        {
            get { return _scope ??= Builder.Build().BeginLifetimeScope(); }
        }

        public override void InitContainer()
        {
            _builder = new ContainerBuilder();

            if (_scope != null)
            {
                _scope.Dispose();
            }

            _scope = null;
        }

        public override void RegisterTransient(Type abstractType, Type concreteType)
        {
            Builder.RegisterType(concreteType).As(abstractType);
        }

        public override void RegisterTransient(string key, Type abstractType, Type concreteType)
        {
            Builder.RegisterType(concreteType)
                .Keyed(key, abstractType)
                .As(abstractType)
                .InstancePerDependency();
        }

        public override void RegisterTransient(Type type)
        {
            Builder.RegisterType(type).InstancePerDependency();
        }

        public override void RegisterInstance(Type abstractType, Object instance)
        {
            Builder.RegisterInstance(instance).As(abstractType);
        }

        public override void RegisterSingleton<I, T>()
        {
            RegisterSingleton(typeof(I), typeof(T));
        }

        public override void RegisterSingleton(Type type)
        {
            RegisterSingleton(type, type);
        }

        public override void RegisterSingleton(Type abstractType, Type concreteType)
        {
            Builder.RegisterType(concreteType).As(abstractType).SingleInstance();
        }

        public override void RegisterSingleton(string key, Type abstractType, Type concreteType)
        {
            Builder.RegisterType(concreteType).Named(key, abstractType)
                .As(abstractType)
                .SingleInstance();
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            Builder.RegisterInstance(instance).As<T>().SingleInstance();
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            Builder.RegisterInstance(instance)
                .Keyed<T>(key).SingleInstance();
        }

        public override T Resolve<T>()
        {
            return Scope.Resolve<T>();
        }

        public override T Resolve<T>(string key)
        {
            return Scope.ResolveNamed<T>(key);
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            return Scope.Resolve<IEnumerable<T>>();
        }

        public override void Release(object component)
        {
            Scope.Dispose();
            _scope = null;
        }

        public override bool IsRegistered<T>()
        {
            try
            {
                Resolve<T>();
                return true;
            }
            catch (ComponentNotRegisteredException)
            {
                return false;
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
            catch (ComponentNotRegisteredException)
            {
                return false;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}