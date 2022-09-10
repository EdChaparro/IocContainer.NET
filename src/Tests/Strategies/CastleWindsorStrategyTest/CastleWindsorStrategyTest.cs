using Castle.Windsor;
using IntrepidProducts.IocContainer.Tests;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.CastleWindsorStrategyTest
{
    [TestClass]
    public class CastleWindsorStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new CastleWindsorStrategy.CastleWindsorStrategy());
        }

        [TestMethod]
        public void ShouldWorkWithAnExistingContainer()
        {
            const string MY_INSTANCE = "MyInstance";
            var originalInstance = new SingletonTestObject { TestProperty = MY_INSTANCE };

            var iocContainer = new CastleWindsorStrategy.CastleWindsorStrategy(new WindsorContainer());

            iocContainer.RegisterInstance<IIocTestSingleton>(originalInstance);

            var instanceFromIoC = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(instanceFromIoC);
            Assert.AreEqual(MY_INSTANCE, instanceFromIoC.TestProperty);
        }

    }
}