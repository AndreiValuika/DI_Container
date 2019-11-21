using DI_ContainerTests;
using FluentAssertions;
using NUnit.Framework;

namespace DI_Container.Tests
{
    [TestFixture()]
    public class DependenceTableTests
    {
        DependencyTable _table;

        [SetUp]
        public void Init()
        {
            _table = new DependencyTable();
            _table.AddDependency(typeof(ITestInterface), typeof(TestClass));
        }
        [Test]
        public void Add_Dependency_Test()
        {
            var actual = _table.GetDependency(typeof(ITestInterface));
            Assert.AreEqual(typeof(TestClass), actual);
        }
        [Test]
        public void Remove_Dependency_Test()
        {
            _table.Remove(typeof(ITestInterface));
            _table.IsPresent(typeof(ITestInterface)).Should().BeFalse();
        }
    }
}