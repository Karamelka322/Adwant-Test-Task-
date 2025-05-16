using System;
using System.Collections.Generic;

namespace CodeBase.Logic.Infrastructure.Container
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly Dictionary<Type, object> _singleDependencies = new(131);
        private readonly Dictionary<Type, List<object>> _recurringDependencies = new(7);
        
        public void Register<TDependency>(object instance)
        {
            if (instance is not TDependency)
            {
                throw new ArgumentException($"instance must be of type {typeof(TDependency).Name}", nameof(instance));
            }
            
            Register(typeof(TDependency), instance);
        }
        
        public void Register<TDependency_1, TDependency_2>(object instance)
        {
            Register<TDependency_1>(instance);
            Register<TDependency_2>(instance);
        }
        
        public void Register(object instance)
        {
            Register(instance.GetType(),  instance);
        }
        
        public TDependency Get<TDependency>()
        {
            if (TryGet<TDependency>(out var sceneDependency))
            {
                return sceneDependency;
            }
            
            throw new NotImplementedException($"Not found dependency {typeof(TDependency).Name}");
        }
        
        public TDependency[] GetAll<TDependency>()
        {
            if (_recurringDependencies.TryGetValue(typeof(TDependency), out var sceneDependencies))
            {
                var array = new TDependency[sceneDependencies.Count];
                
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = (TDependency)sceneDependencies[i];
                }
                
                return array;
            }
            
            return Array.Empty<TDependency>();
        }
        
        private void Register(Type type, object instance)
        {
            if (_recurringDependencies.ContainsKey(type))
            {
                _recurringDependencies[type].Add(instance);
                return;
            }

            if (_singleDependencies.ContainsKey(type))
            {
                _singleDependencies.Remove(type);
                
                var list = new List<object>(10) { instance };
                _recurringDependencies.Add(type, list);
            }
            else
            {
                _singleDependencies.Add(type, instance);
            }
        }
        
        private bool TryGet<TDependency>(out TDependency dependency)
        {
            if (_singleDependencies.TryGetValue(typeof(TDependency), out var implementation))
            {
                dependency = (TDependency)implementation;
                return true;
            }
            
            dependency = default;
            return false;
        }
    }
}