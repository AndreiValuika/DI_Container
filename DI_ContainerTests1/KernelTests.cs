using DI_ContainerTests;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace DI_Container.Tests
{


    [TestFixture]
    public class KernelTests
    {
        class TestClass_WithOut { }

        Mock<IDependencyTable> _mock;
        Kernel _kernel;

        [SetUp]
        public void Init()
        {
            _mock = new Mock<IDependencyTable>();
            _kernel = new Kernel(_mock.Object);
        }
        [Test()]
        public void AddDependency_TypeOf_Positive_Test()
        {
            _kernel.AddDependency(typeof(ITestInterface), typeof(TestClass));
            _mock.Verify(_ => _.AddDependency(typeof(ITestInterface), typeof(TestClass)), Times.Once);
        }
        [Test]
        public void AddDependency_TypeOf_Negative_Test()
        {
            Assert.Throws<InvalidCastException>(() => _kernel.AddDependency(typeof(ITestInterface), typeof(TestClass_WithOut)));
        }

        [Test]
        public void AddDependency_Generic_Positive_Test()
        {
            _kernel.AddDependency(typeof(ITestInterface), typeof(TestClass));
            _mock.Verify(_ => _.AddDependency(typeof(ITestInterface), typeof(TestClass)), Times.Once);
        }

        [Test]
        public void Get_TypeOf_Test()
        {
            _kernel.AddDependency(typeof(ITestInterface), typeof(TestClass));
            _mock.Setup(_dependencyTable => _dependencyTable.GetDependency(typeof(ITestInterface)))
                                                            .Returns(typeof(TestClass));
            _mock.Setup(_ => _.IsPresent(typeof(ITestInterface))).Returns(true);
            Object ob = _kernel.Get(typeof(ITestInterface));
            ob.Should().BeOfType<TestClass>();
        }
        [Test]
        public void Get_Generic_Test()
        {
            _kernel.AddDependency<ITestInterface, TestClass>();
            _mock.Setup(_dependencyTable => _dependencyTable.GetDependency(typeof(ITestInterface)))
                                                            .Returns(typeof(TestClass));
            _mock.Setup(_ => _.IsPresent(typeof(ITestInterface))).Returns(true);
            Object ob = _kernel.Get<ITestInterface>();
            ob.Should().BeOfType<TestClass>();
        }
        [Test]
        public void Get_Extend_Exception()
        {
            _mock.Setup(_ => _.IsPresent(typeof(ITestInterface))).Returns(false);
            Assert.Throws<Exception>(() => _kernel.Get<ITestInterface>());
        }
    }
}