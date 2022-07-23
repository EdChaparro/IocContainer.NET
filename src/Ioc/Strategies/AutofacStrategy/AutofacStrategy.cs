using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core.Registration;
using IntrepidProducts.IocContainer;

namespace IntrepidProducts.Ioc.AutofacStrategy
{
    public class AutofacStrategy : StrategyAbstract
    {
        private ContainerBuilder _builder = new ContainerBuilder();

        private ILifetimeScope _scope;
        private ILifetimeScope Scope
        {
            get
            {
                if (_scope == null)
                {
                    _scope = _builder.Build().BeginLifetimeScope();
                }

                return _scope;
            }
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
            _builder.RegisterType(concreteType).As(abstractType);
        }

        public override void RegisterTransient(string key, Type abstractType, Type concreteType)
        {
            _builder.RegisterType(concreteType)
                .Keyed(key, abstractType)
                .As(abstractType)
                .InstancePerDependency();
        }

        public override void RegisterTransient(Type type)
        {
            _builder.RegisterType(type).InstancePerDependency();
        }

        public override void RegisterInstance(Type abstractType, Object instance)
        {
            _builder.RegisterInstance(instance).As(abstractType);
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
            _builder.RegisterType(concreteType).As(abstractType).SingleInstance();
        }

        public override void RegisterSingleton(string key, Type abstractType, Type concreteType)
        {
            _builder.RegisterType(concreteType).Named(key, abstractType)
                .As(abstractType)
                .SingleInstance();
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            _builder.RegisterInstance(instance).As<T>().SingleInstance();
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            _builder.RegisterInstance(instance)
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