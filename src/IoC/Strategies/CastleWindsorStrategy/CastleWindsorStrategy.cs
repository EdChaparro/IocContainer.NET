using System;
using System.Collections.Generic;
using Castle.Core;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using IntrepidProducts.IocContainer;

namespace IntrepidProducts.IoC.CastleWindsorStrategy
{
    public class CastleWindsorStrategy : StrategyAbstract
    {
        public CastleWindsorStrategy(WindsorContainer? container = null)
        {
            _container = container;
        }

        private WindsorContainer? _container;
        private WindsorContainer Container
        {
            get { return _container ??= new WindsorContainer(); }
        }

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
            Container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType)
                .Named(key).LifeStyle
                .Is(LifestyleType.Transient));
        }

        public override void RegisterTransient(Type type)
        {
            RegisterTransient(type.ToString(), type, type);
        }

        public override void RegisterInstance(Type abstractType, Object instance)
        {
            Container.Register(Component.For(abstractType).Instance(instance));
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
            Container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType)
                .LifeStyle.Is(LifestyleType.Singleton));
        }

        public override void RegisterSingleton(string key, Type abstractType, Type concreteType)
        {
            Container.Register(Component.For(abstractType)
                .ImplementedBy(concreteType).Named(key)
                .LifeStyle.Is(LifestyleType.Singleton));
        }

        public override void RegisterInstance<T>(T instance) where T : class
        {
            Container.Register(Component.For<T>().Instance(instance));
        }

        public override void RegisterInstance<T>(string key, T instance) where T : class
        {
            Container.Register(Component.For<T>().Named(key).Instance(instance));
        }

        public override T Resolve<T>()
        {
            return (T)Container.Resolve(typeof (T));
        }

        public override T Resolve<T>(string key)
        {
            return Container.Resolve<T>(key);
        }

        public override IEnumerable<T> ResolveAll<T>()
        {
            return Container.Kernel.ResolveAll<T>();
        }

        public override void Release(object component)
        {
            Container.Release(component);
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