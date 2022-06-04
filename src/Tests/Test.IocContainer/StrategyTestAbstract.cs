using System.Linq;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests
{
    [TestClass]
    public abstract class StrategyTestAbstract
    {
        protected IocTestFactory IocTestFactory { get; set; }

        [TestMethod]
        public void TestCheckForDuplicatesInitiallyFalse()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsFalse(iocContainer.IgnoreDuplicateRegisterRequests);
        }

        [TestMethod]
        public void TestIsRegistered()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsTrue(iocContainer.IsRegistered(typeof(ITransientTest)));
            iocContainer.InitContainer();
            Assert.IsFalse(iocContainer.IsRegistered(typeof(ITransientTest)));
        }

        [TestMethod]
        public void TestIsRegisteredWithKey()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            iocContainer.InitContainer();
            const string KEY = "MyKey";

            Assert.IsFalse(iocContainer.IsRegistered(KEY));

            iocContainer.Register(KEY, typeof(ITransientTest), typeof(TransientTestObject));
            Assert.IsTrue(iocContainer.IsRegistered(KEY));
        }

        [TestMethod]
        public void TestIgnoreDuplicateRegistrations()
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

            iocContainer.IgnoreDuplicateRegisterRequests = true;
            iocContainer.Register<IIocTestSingleton, SingletonTestObject>(); //Container should ignore this Register request
            testObject = iocContainer.Resolve<IIocTestSingleton>();
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values
        }

        [TestMethod]
        public void TestIgnoreDuplicateRegistrationsWithUnresolvedDependency()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            iocContainer.InitContainer();

            iocContainer.Register(typeof(ISingletonObjectWithDependencyTest), typeof(SingletonTestObjectWithDependency));

            iocContainer.IgnoreDuplicateRegisterRequests = true;
            iocContainer.Register(typeof(ISingletonObjectWithDependencyTest), typeof(SingletonTestObjectWithDependency)); //Should not throw an exception
            iocContainer.IgnoreDuplicateRegisterRequests = false;

            iocContainer.Register(typeof(ISingletonTestDependencyObject), 
                                typeof(SingletonTestDependencyObject));


            var testObject = iocContainer.Resolve<ISingletonObjectWithDependencyTest>();
            Assert.IsNotNull(iocContainer);
            Assert.IsNotNull(testObject.MyDependency);
        }

        [TestMethod]
        public void TestIgnoreDuplicateRegistrationsWithKey()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            const string KEY = "Singleton";
            var testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values    

            iocContainer.IgnoreDuplicateRegisterRequests = true;
            iocContainer.Register<IIocTestSingleton, SingletonTestObject>(KEY); //Container should ignore this Register request
            testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values    
        }

        [TestMethod]
        public void TestTransientInstance()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            var testObject = iocContainer.Resolve<ITransientTest>();
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>();
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        [TestMethod]
        public void TestTransientWithKey()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            const string KEY = "Transient";
            var testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<ITransientTest>(KEY);
            Assert.IsNull(testObject.TestProperty); //Transient value doesn't retain property values
        }

        [TestMethod]
        public void TestSingletonInstance()
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
        }

        [TestMethod]
        public void TestSingletonInstanceWithKey()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            Assert.IsNotNull(iocContainer);

            const string KEY = "Singleton";
            var testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.IsNotNull(iocContainer);
            Assert.IsNull(testObject.TestProperty);

            const string TEST_VALUE = "TestValue";
            testObject.TestProperty = TEST_VALUE;

            testObject = iocContainer.Resolve<IIocTestSingleton>(KEY);
            Assert.AreEqual(TEST_VALUE, testObject.TestProperty); //Singleton retains property values
        }

        [TestMethod]
        public void TestRegisterInstance()
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
        public void TestRegisterInstanceWithKey()
        {
            const string MY_INSTANCE1_KEY = "MyInstance1";
            const string MY_INSTANCE2_KEY = "MyInstance2";
            var originalInstance1 = new SingletonTestObject { TestProperty = MY_INSTANCE1_KEY };
            var originalInstance2 = new SingletonTestObject { TestProperty = MY_INSTANCE2_KEY };

            var iocContainer = IocFactoryAbstract.GetContainer();
            iocContainer.InitContainer();
            iocContainer.RegisterInstance<IIocTestSingleton>(MY_INSTANCE1_KEY, originalInstance1);
            iocContainer.RegisterInstance<IIocTestSingleton>(MY_INSTANCE2_KEY, originalInstance2);

            var instance1FromIoC = iocContainer.Resolve<IIocTestSingleton>(MY_INSTANCE1_KEY);
            var instance2FromIoC = iocContainer.Resolve<IIocTestSingleton>(MY_INSTANCE2_KEY);
            Assert.IsNotNull(instance1FromIoC);
            Assert.IsNotNull(instance2FromIoC);
            Assert.AreEqual(MY_INSTANCE1_KEY, instance1FromIoC.TestProperty);
            Assert.AreEqual(MY_INSTANCE2_KEY, instance2FromIoC.TestProperty);
        }

        [TestMethod]
        public void ResolveAll()
        {
            var iocContainer = IocFactoryAbstract.GetContainer();
            var services = iocContainer.ResolveAll(typeof (ITransientTest)).ToList();

            Assert.IsTrue((from s in services
                where s.GetType() == typeof(TransientTestObject)
                select s).Any());

            Assert.IsTrue((from s in services
                where s.GetType() == typeof(TransientTestObject2)
                select s).Any());
        }
    }
}