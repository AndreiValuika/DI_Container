using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI_Container
{
    public class Kernel
    {
        private readonly IDependencyTable _dependencyTable;
        public Kernel(IDependencyTable dependencyTable)
        {
            _dependencyTable = dependencyTable;
        }
        /// <summary>
        /// Create dependency between two types
        /// </summary>
        /// <param name="abstraction"></param>
        /// <param name="realization"></param>
        public void AddDependency(Type abstraction, Type realization)
        {
                if (!abstraction.IsAssignableFrom(realization))
                {
                    throw new InvalidCastException($"{realization.Name} doesn't implement {abstraction.Name}");
                }
                _dependencyTable.AddDependency(abstraction, realization);
        }
        /// <summary>
        /// Close class to himself.
        /// </summary>
        /// <param name="abstraction"></param>
        public void AddDependency(Type abstraction)
        {
            if (!abstraction.IsClass)
            {
                throw new ArgumentException("Must be 'class' Type");
            } 
            _dependencyTable.AddDependency(abstraction, abstraction);
        }
        /// <summary>
        /// Close class to himself.
        /// </summary>
        /// <param name="abstraction"></param>
        public void AddDependency<T>() where T : new ()
        {
           this.AddDependency(typeof(T));
        }

        /// <summary>
        /// Create dependency between two types
        /// </summary>
        /// <param name="abstraction"></param>
        /// <param name="realization"></param>
        public void AddDependency<T, V>() where V : T
        {
            _dependencyTable.AddDependency(typeof(T), typeof(V));
        }
        /// <summary>
        /// Return new object required class  
        /// </summary>
        /// <param name="abstration"></param>
        /// <returns></returns>
        public object Get(Type abstration) 
        {
            ConstructorInfo constructorInfo;
            List<object> arguments = new List<object>();
           
                if (!_dependencyTable.IsPresent(abstration))
                {
                    throw new Exception("Dependency not present");
                }
                constructorInfo = _dependencyTable.GetDependency(abstration).GetConstructors().First();
                
                foreach (ParameterInfo item in constructorInfo.GetParameters())
                {
                    arguments.Add(Get(item.ParameterType));
                }
            return constructorInfo.Invoke(arguments.ToArray()); ;
        }
        /// <summary>
        /// Return new object required class  
        /// </summary>
        /// <param name="abstration"></param>
        /// <returns></returns>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }
    }
}
