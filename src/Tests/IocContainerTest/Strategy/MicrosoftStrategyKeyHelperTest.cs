using IntrepidProducts.IoC.MicrosoftStrategy;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests.Strategy
{
    [TestClass]
    public class MicrosoftStrategyKeyHelperTest
    {
        [TestMethod]
        public void ShouldMapObjectToKey()
        {
            var helper = new MicrosoftStrategyKeyHelper();

            const string KEY1 = "K1";
            const string KEY2 = "K2";

            helper.Register<ITransientTest, TransientTestObject>(KEY1);
            helper.Register<ITransientTest, TransientTestObject2>(KEY2);

            Assert.AreEqual(1, helper.TypesRegisteredCount);
            Assert.AreEqual(2, helper.KeyCountFor<ITransientTest>());

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<TransientTestObject>();
            serviceCollection.AddTransient<TransientTestObject2>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var object1 = helper.Resolve<ITransientTest>(serviceProvider, KEY1);
            var object2 = helper.Resolve<ITransientTest>(serviceProvider, KEY2);

            Assert.IsNotNull(object1);
            Assert.IsNotNull(object2);

            Assert.AreEqual(typeof(TransientTestObject), object1.GetType());
            Assert.AreEqual(typeof(TransientTestObject2), object2.GetType());
        }

        [TestMethod]
        public void ShouldMapInstancesToKey()
        {
            var helper = new MicrosoftStrategyKeyHelper();

            var serviceCollection = new ServiceCollection();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            const string MY_INSTANCE1_KEY = "MyInstance1";
            const string MY_INSTANCE2_KEY = "MyInstance2";
            var originalInstance1 = new SingletonTestObject { TestProperty = MY_INSTANCE1_KEY };
            var originalInstance2 = new SingletonTestObject { TestProperty = MY_INSTANCE2_KEY };

            helper.RegisterInstance<IIocTestSingleton>(MY_INSTANCE1_KEY, originalInstance1);
            helper.RegisterInstance<IIocTestSingleton>(MY_INSTANCE2_KEY, originalInstance2);

            var instance1FromIoC = helper.Resolve<IIocTestSingleton>(serviceProvider, MY_INSTANCE1_KEY);
            var instance2FromIoC = helper.Resolve<IIocTestSingleton>(serviceProvider, MY_INSTANCE2_KEY);

            Assert.IsNotNull(instance1FromIoC);
            Assert.IsNotNull(instance2FromIoC);
            Assert.AreEqual(MY_INSTANCE1_KEY, instance1FromIoC.TestProperty);
            Assert.AreEqual(MY_INSTANCE2_KEY, instance2FromIoC.TestProperty);
        }
    }
}