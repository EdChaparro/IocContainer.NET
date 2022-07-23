using System.Linq;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests
{
    [TestClass]
    public abstract class StrategyTestAbstract
    {
        protected IocTestFactory IocTestFactory { get; set; }

        #region IsRegistered
        [TestMethod]
        public void ShouldReportWhetherInterfaceIsRegistered()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            //Nothing registered...
            Assert.IsFalse(iocContainer.IsRegistered<ITransientTest>());
            iocContainer.InitContainer();

            iocContainer.RegisterTransient
            (typeof(ITransientTest),
                typeof(TransientTestObject));

            Assert.IsTrue(iocContainer.IsRegistered<ITransientTest>());
        }

        [TestMethod]
        public void ShouldReportWhetherKeyIsRegistered()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            const string KEY = "MyKey";

            Assert.IsFalse(iocContainer.IsRegistered<ITransientTest>(KEY));
            iocContainer.InitContainer();

            iocContainer.RegisterSingleton(KEY,
                typeof(ITransientTest), typeof(TransientTestObject));

            Assert.IsTrue(iocContainer.IsRegistered<ITransientTest>(KEY));
        }
        #endregion

        #region Register Transient
        [TestMethod]
        public void ShouldInstantiateNewInstancesWhenTransientResolvedWithInterfaceRegisteredWithTypeObjects()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            iocContainer.RegisterTransient
            (typeof(ITransientTest),
                typeof(TransientTestObject));

            var testObject = iocContainer.Resolve<ITransientTest>();

            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>();
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        [TestMethod]
        public void ShouldInstantiateNewInstancesWhenTransientResolvedWithInterfaceRegisteredGenerically()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            iocContainer.RegisterTransient<ITransientTest, TransientTestObject>();

            var testObject = iocContainer.Resolve<ITransientTest>();

            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>();
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        [TestMethod]
        public void ShouldInstantiateNewInstancesWhenTransientResolvedWithKeyRegisteredWithTypeObjects()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            const string KEY = "Transient";

            iocContainer.RegisterTransient(KEY,
                typeof(ITransientTest),
                typeof(TransientTestObject));

            var testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        [TestMethod]
        public void ShouldInstantiateNewInstancesWhenTransientResolvedWithKeyRegisteredWithGenerically()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            const string KEY = "Transient";

            iocContainer.RegisterTransient<ITransientTest, TransientTestObject>(KEY);

            var testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        #endregion

        #region Register Singleton
        [TestMethod]
        public void ShouldInstantiateSameInstancesWhenSingletonResolvedWithInterface()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            iocContainer.RegisterSingleton
                (typeof(IIocTestSingleton),
                    typeof(SingletonTestObject));

            var testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values
        }
        #endregion

        #region Register Instance
        [TestMethod]
        public void ShouldInstantiateSameInstancesWhenSingletonResolvedWithKey()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            const string KEY = "Singleton";

            iocContainer.RegisterSingleton(KEY,
                typeof(IIocTestSingleton),
                typeof(SingletonTestObject));

            var testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.IsNotNull(testObject);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values
        }

        [TestMethod]
        public void ShouldResolveToProvidedInstanceWhenResolvedWithInterface()
        {
            const string MY_INSTANCE = "MyInstance";
            var originalInstance = new SingletonTestObject { TestProperty = MY_INSTANCE };

            var iocContainer = IocFactoryAbstract.GetContainer();
            iocContainer.InitContainer();
            iocContainer.RegisterInstance<IIocTestSingleton>(originalInstance);

            var instanceFromIoC = iocContainer.Resolve<IIocTestSingleton>();
            Assert.IsNotNull(instanceFromIoC);
            Assert.AreEqual(MY_INSTANCE, instanceFromIoC.TestProperty);
        }

        [TestMethod]
        public void ShouldResolveToProvidedInstanceWhenResolvedWithKey()
        {
            const string MY_INSTANCE1_KEY = "MyInstance1";
            const string MY_INSTANCE2_KEY = "MyInstance2";
            var originalInstance1 = new SingletonTestObject { TestProperty = MY_INSTANCE1_KEY };
            var originalInstance2 = new SingletonTestObject { TestProperty = MY_INSTANCE2_KEY };

            var iocContainer = IocFactoryAbstract.GetContainer();
            iocContainer.InitContainer();
            iocContainer.RegisterInstance<IIocTestSingleton>
                (MY_INSTANCE1_KEY,originalInstance1);

            iocContainer.RegisterInstance<IIocTestSingleton>
                (MY_INSTANCE2_KEY, originalInstance2);

            var instance1FromIoC = iocContainer.Resolve<IIocTestSingleton>(MY_INSTANCE1_KEY);
            var instance2FromIoC = iocContainer.Resolve<IIocTestSingleton>(MY_INSTANCE2_KEY);
            Assert.IsNotNull(instance1FromIoC);
            Assert.IsNotNull(instance2FromIoC);
            Assert.AreEqual(MY_INSTANCE1_KEY, instance1FromIoC.TestProperty);
            Assert.AreEqual(MY_INSTANCE2_KEY, instance2FromIoC.TestProperty);
        }
        #endregion

        [TestMethod]
        public void ShouldProvideCollectionOfAllObjectsImplementedByAnInterface()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();

            iocContainer.RegisterTransient("ResolveAllTestObject",
                typeof(ITransientTest),
                typeof(TransientTestObject2));

            iocContainer.RegisterTransient("Transient",
                typeof(ITransientTest),
                typeof(TransientTestObject));

            var services = iocContainer.ResolveAll<ITransientTest>()
                .ToList();

            Assert.IsTrue((from s in services
                           where s.GetType() == typeof(TransientTestObject)
                           select s).Any());

            Assert.IsTrue((from s in services
                           where s.GetType() == typeof(TransientTestObject2)
                           select s).Any());
        }
    }
}