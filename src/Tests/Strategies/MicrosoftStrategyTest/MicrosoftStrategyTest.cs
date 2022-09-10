using IntrepidProducts.IocContainer.Tests;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.MicrosoftStrategyTest
{
    [TestClass]
    public class MicrosoftStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new MicrosoftStrategy.MicrosoftStrategy());
        }

        [TestMethod]
        public void ShouldWorkWithAnExistingContainer()
        {
            const string MY_INSTANCE = "MyInstance";
            var originalInstance = new SingletonTestObject { TestProperty = MY_INSTANCE };

            var iocContainer = new MicrosoftStrategy.MicrosoftStrategy(new ServiceCollection());

            iocContainer.RegisterInstance<IIocTestSingleton>(originalInstance);

            var instanceFromIoC = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(instanceFromIoC);
            Assert.AreEqual(MY_INSTANCE, instanceFromIoC.TestProperty);
        }
    }
}