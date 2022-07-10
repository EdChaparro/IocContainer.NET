using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core.Registration;

namespace IntrepidProducts.IocContainer.Strategy
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

        public override void Register(Type abstractType, Object obj)
        {
            _builder.RegisterInstance(obj).As(abstractType);
        }

        public override void Register(Type abstractType, Type concreteType, bool useDefaultConstructor)
        {
            Register(abstractType, concreteType);
        }

        public override void Register<I, T>()
        {
            Register(typeof(I), typeof(T));
        }

        public override void Register(Type type)
        {
            Register(type, type);
        }

        public override void Register(Type abstractType, Type concreteType)
        {
            _builder.RegisterType(concreteType).As(abstractType).InstancePerLifetimeScope();
        }

        public override void Register(string key, Type abstractType, Type concreteType)
        {
            _builder.RegisterType(concreteType).Named(key, abstractType)
                .As(abstractType)
                .InstancePerLifetimeScope();
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

        public override void RegisterInstance(Type fromType, object instance)
        {
            _builder.RegisterInstance(instance).As(fromType)
                .InstancePerMatchingLifetimeScope();
        }

        public override T Resolve<T>()
        {
            return Scope.Resolve<T>();
        }

        public override T Resolve<T>(string key)
        {
            return Scope.ResolveNamed<T>(key);
        }

        public override object Resolve(Type type)
        {
            return Scope.Resolve(type);
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            return Scope.Resolve<IEnumerable<T>>();
        }

        public override void Release(object component)
        {
            Scope.Dispose();
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

        public override bool IsRegistered(Type type)
        {
            try
            {
                Resolve(type);
                return true;
            }
            catch (ComponentNotRegisteredException)
            {
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