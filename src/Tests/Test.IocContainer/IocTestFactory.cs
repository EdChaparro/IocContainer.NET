using IntrepidProducts.IocContainer.Tests.TestObjects;

namespace IntrepidProducts.IocContainer.Tests
{
    public class IocTestFactory : IocFactoryAbstract
    {
        public IocTestFactory(IIocContainer iocContainer) : base(iocContainer)
        {}

        public override void RegisterClasses()
        {
            IocContainer.RegisterTransient(typeof(ITransientTest), typeof(TransientTestObject));
            IocContainer.RegisterTransient("ResolveAllTestObject", typeof(ITransientTest), typeof(TransientTestObject2));
            IocContainer.RegisterTransient("Transient", typeof(ITransientTest), typeof(TransientTestObject));

            IocContainer.Register(typeof(IIocTestSingleton), typeof(SingletonTestObject));
            IocContainer.Register("Singleton", typeof(IIocTestSingleton), typeof(SingletonTestObject));
         }
    }
}