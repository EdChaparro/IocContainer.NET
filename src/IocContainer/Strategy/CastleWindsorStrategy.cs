using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace IntrepidProducts.IocContainer.Strategy
{
    public class CastleWindsorStrategy : StrategyAbstract
    {
        private WindsorContainer _container = new WindsorContainer();

        public override void InitContainer()
        {
            _container = new WindsorContainer();
        }

        public override void RegisterTransient(Type fromType, Type concreteType)
        {
            if (IgnoreRegisterRequest(fromType))
            {
                return;
            }

            RegisterTransient(fromType.ToString(), fromType, concreteType);
        }

        public override void RegisterTransient(string key, Type fromType, Type concreteType)
        {
            if (IgnoreRegisterRequest(key))
            {
                return;
            }

            _container.Register(Component.For(fromType).ImplementedBy(concreteType).Named(key).LifeStyle.Is(LifestyleType.Transient));
        }

        public override void RegisterTransient(Type type)
        {
            RegisterTransient(type.ToString(), type, type);
        }

        public override void Register(Type fromType, Object obj)
        {
            if (IgnoreRegisterRequest(fromType))
            {
                return;
            }

            _container.Register(Component.For(fromType).ImplementedBy(obj.GetType()).Named(fromType.ToString()));
        }

        public override void Register(Type fromType, Type concreteType, bool useDefaultConstructor)
        {
            Register(fromType, concreteType);
        }

        public override void Register<I, T>()
        {
            Register(typeof(I), typeof(T));
        }

        public override void Register(Type type)
        {
            Register(type, type);
        }

        public override void Register(Type fromType, Type concreteType)
        {
            Register(fromType.ToString(), fromType, concreteType);
        }

        public override void Register(string key, Type fromType, Type concreteType)
        {
            if (IgnoreRegisterRequest(key))
            {
                return;
            }

            _container.Register(Component.For(fromType).ImplementedBy(concreteType).Named(key).LifeStyle.Is(LifestyleType.Singleton));
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            var fromType = typeof(T);

            if (IgnoreRegisterRequest(fromType))
            {
                return;
            }

            _container.Register(Component.For<T>().Instance(instance));
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            if (IgnoreRegisterRequest(key))
            {
                return;
            }

            _container.Register(Component.For<T>().Named(key).Instance(instance));
        }

        public override void RegisterInstance(Type fromType, object instance)
        {
            if (IgnoreRegisterRequest(fromType))
            {
                return;
            }

            _container.Register(Component.For(fromType).Instance(instance));
        }

        public override T Resolve<T>()
        {
            return (T)_container.Resolve(typeof (T));
        }

        public override T Resolve<T>(string key)
        {
            return _container.Resolve<T>(key);
        }

        public override object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public override IEnumerable<object> ResolveAll(Type type)
        {
            return _container.Kernel.ResolveAll(type).Cast<object>();
        }

        public override void Release(object component)
        {
            _container.Release(component);           
        }

        public override bool IsRegistered(Type type)
        {
            try
            {
                Resolve(type);
                return true;
            }
            catch (HandlerException)
            {
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public override bool IsRegistered(string key)
        {
            try
            {
                Resolve<object>(key);
                return true;
            }
            catch (HandlerException)
            {
                return true;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}