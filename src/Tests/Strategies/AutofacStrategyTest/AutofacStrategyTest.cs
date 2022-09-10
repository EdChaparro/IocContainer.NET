using Autofac;
using IntrepidProducts.IocContainer.Tests;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.AutofacStrategyTest
{
    [TestClass]
    public class AutofacStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new AutofacStrategy.AutofacStrategy());
        }

        [TestMethod]
        public void ShouldWorkWithAnExistingContainer()
        {
            const string MY_INSTANCE = "MyInstance";
            var originalInstance = new SingletonTestObject { TestProperty = MY_INSTANCE };

            var iocContainer = new AutofacStrategy.AutofacStrategy(new ContainerBuilder());

            iocContainer.RegisterInstance<IIocTestSingleton>(originalInstance);

            var instanceFromIoC = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(instanceFromIoC);
            Assert.AreEqual(MY_INSTANCE, instanceFromIoC.TestProperty);
        }
    }
}