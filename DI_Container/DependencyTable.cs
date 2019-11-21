namespace DI_Container
{
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// 
    /// </summary>
    public class DependencyTable : IDependencyTable
    {
        private readonly ConcurrentDictionary<Type, Type> _countries;

        public DependencyTable()
        {
            _countries = new ConcurrentDictionary<Type, Type>();
        }

        public void AddDependency(Type abstraction, Type realization)
        {
            _countries.TryAdd(abstraction, realization);
        }
        public Type GetDependency(Type abstraction)
        {
            return _countries[abstraction];
        }
        public void Remove(Type abstraction)
        {
            _countries.TryRemove(abstraction,out _);
        }
        public bool IsPresent(Type type)
        {
            return _countries.ContainsKey(type);
        }
    }
}
