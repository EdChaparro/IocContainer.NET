using System;
using System.Collections.Generic;
using System.Linq;
using IntrepidProducts.IocContainer;

namespace IntrepidProducts.Ioc.MicrosoftStrategy
{
    public class MicrosoftStrategyKeyHelper
    {
        private readonly Dictionary<Type, Dictionary<string, Type>> _mappings
            = new Dictionary<Type, Dictionary<string, Type>>();

        private readonly Dictionary<Type, Dictionary<string, object>> _instanceMappings
            = new Dictionary<Type, Dictionary<string, object>>();

        public void RegisterInstance<TAbstract>(string key, object instance) where TAbstract : class
        {
            var abstractType = typeof(TAbstract);

            if (!_instanceMappings.ContainsKey(abstractType))
            {
                _instanceMappings[abstractType] = new Dictionary<string, object>();
            }

            var map = _instanceMappings[abstractType];
            map[key.ToUpper()] = instance;
        }

        public void Register<TAbstract, TConcrete>(string key) where TConcrete : TAbstract
        {
            Register(typeof(TAbstract), typeof(TConcrete), key);
        }

        public void Register(Type abstractType, Type concreteType, string key)
        {
            if (!_mappings.ContainsKey(abstractType))
            {
                _mappings[abstractType] = new Dictionary<string, Type>();
            }

            var map = _mappings[abstractType];
            map[key.ToUpper()] = concreteType;
        }

        public TAbstract Resolve<TAbstract>(IServiceProvider serviceProvider, string key)
        {
            return (TAbstract)Resolve(serviceProvider, typeof(TAbstract), key);
        }

        public object Resolve(IServiceProvider serviceProvider, Type abstractType, string key)
        {
            if ((!_mappings.ContainsKey(abstractType)) &&
                (!_instanceMappings.ContainsKey(abstractType)))
            {
                throw new NotRegisteredException(abstractType);
            }

            var rObject = FindMappedObject(serviceProvider, abstractType, key);

            if (rObject != null)
            {
                return rObject;
            }

            var iObject = FindInstanceObject(abstractType, key);

            if (iObject == null)
            {
                throw new NotRegisteredException(abstractType, $"key: {key}");
            }

            return iObject;
        }

        private object FindMappedObject(IServiceProvider serviceProvider, Type abstractType, string key)
        {
            if (!_mappings.ContainsKey(abstractType))
            {
                return null;
            }

            var map = _mappings[abstractType];

            return serviceProvider.GetService(map[key.ToUpper()]);
        }

        private object FindInstanceObject(Type abstractType, string key)
        {
            if (!_instanceMappings.ContainsKey(abstractType))
            {
                return null;
            }

            var map = _instanceMappings[abstractType];

            return map[key.ToUpper()];
        }

        public int TypesRegisteredCount => _mappings.Count + _instanceMappings.Count;

        public int KeyCountFor<TAbstract>()
        {
            var type = typeof(TAbstract);

            var mappingsCount = 0;
            var instanceMappingsCount = 0;

            if (_mappings.ContainsKey(type))
            {
                mappingsCount = _mappings[type].Count;
            }

            if (_instanceMappings.ContainsKey(type))
            {
                instanceMappingsCount = _instanceMappings[type].Count;
            }

            return mappingsCount + instanceMappingsCount;
        }

        public bool IsRegistered<TAbstract>(string key)
        {
            if (!_mappings.ContainsKey(typeof(TAbstract)))
            {
                return false;
            }

            var map = _mappings[typeof(TAbstract)];

            return map.ContainsKey(key.ToUpper());
        }

        public IEnumerable<TAbstract> ResolveAll<TAbstract>(IServiceProvider serviceProvider)
        {
            var mappedObjects = FindAllMappingsFor<TAbstract>(serviceProvider);
            var instanceObjects = FindAllInstancesFor<TAbstract>();

            return mappedObjects.Union(instanceObjects);
        }

        private IEnumerable<TAbstract> FindAllMappingsFor<TAbstract>(IServiceProvider serviceProvider)
        {
            var objects = new List<TAbstract>();

            var type = typeof(TAbstract);

            if (!_mappings.ContainsKey(type))
            {
                return objects;
                ;
            }

            var map = _mappings[type];

            foreach (var key in map.Keys)
            {
                var mObject = (TAbstract)FindMappedObject(serviceProvider, type, key);

                if (mObject != null)
                {
                    objects.Add(mObject);
                }
            }

            return objects;
        }

        private IEnumerable<TAbstract> FindAllInstancesFor<TAbstract>()
        {
            var objects = new List<TAbstract>();

            var type = typeof(TAbstract);

            if (!_instanceMappings.ContainsKey(type))
            {
                return objects; ;
            }

            var map = _instanceMappings[type];

            foreach (var key in map.Keys)
            {
                var mObject = (TAbstract)FindInstanceObject(type, key);

                if (mObject != null)
                {
                    objects.Add(mObject);
                }
            }

            return objects;
        }
    }
}