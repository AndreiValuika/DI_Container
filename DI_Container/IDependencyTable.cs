using System;

namespace DI_Container
{
    public interface IDependencyTable
    {
        bool IsPresent(Type type);
        void Remove(Type abstraction);
        Type GetDependency(Type abstraction);
        void AddDependency(Type abstraction, Type realization);
    }
}
