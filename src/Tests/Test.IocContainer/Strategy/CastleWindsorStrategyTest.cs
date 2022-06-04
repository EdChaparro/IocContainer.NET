using Castle.MicroKernel;
using IntrepidProducts.IocContainer.Strategy;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests.Strategy
{
    [TestClass]
    public class CastleWindsorStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new CastleWindsorStrategy());
            IocTestFactory.RegisterClasses();
        }

        [TestMethod]
        [ExpectedException(typeof(ComponentRegistrationException))]
        public void TestIgnoreDuplicateRegistrationWhenFalse()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            var testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values

            //Register a second time
            iocContainer.Register<IIocTestSingleton, SingletonTestObject>(); //This should throw an exception
            testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values
        }
    }
}