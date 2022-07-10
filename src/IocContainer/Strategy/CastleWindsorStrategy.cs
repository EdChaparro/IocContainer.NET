using System;
using System.Collections.Generic;
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

        public override void RegisterTransient(Type abstractType, Type concreteType)
        {
            RegisterTransient(abstractType.ToString(), abstractType, concreteType);
        }

        public override void RegisterTransient(string key, Type abstractType, Type concreteType)
        {
            _container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType)
                .Named(key).LifeStyle
                .Is(LifestyleType.Transient));
        }

        public override void RegisterTransient(Type type)
        {
            RegisterTransient(type.ToString(), type, type);
        }

        public override void Register(Type abstractType, Object obj)
        {
            _container.Register(Component.For(abstractType).ImplementedBy(obj.GetType()).Named(abstractType.ToString()));
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
            _container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType)
                .LifeStyle.Is(LifestyleType.Singleton));
        }

        public override void Register(string key, Type abstractType, Type concreteType)
        {
            _container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType).Named(key)
                .LifeStyle.Is(LifestyleType.Singleton));
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            var fromType = typeof(T);

            _container.Register(Component.For<T>().Instance(instance));
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            _container.Register(Component.For<T>().Named(key).Instance(instance));
        }

        public override void RegisterInstance(Type fromType, object instance)
        {
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

        public override IEnumerable<T> ResolveAll<T>()
        {
            return _container.Kernel.ResolveAll<T>();
        }

        public override void Release(object component)
        {
            _container.Release(component);           
        }

        public override bool IsRegistered<T>()
        {
            try
            {
                Resolve<T>();
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

        public override bool IsRegistered<T>(string key)
        {
            try
            {
                Resolve<T>(key);
                return true;
            }
            catch (HandlerException)
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