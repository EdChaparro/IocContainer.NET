namespace IntrepidProducts.IocContainer.Tests
{
    public class IocTestFactory : IocFactoryAbstract
    {
        public IocTestFactory(IIocContainer iocContainer) : base(iocContainer)
        {}

        public override void RegisterClasses()
        {
            //Moved registrations to individual unit tests to improve clarity
        }
    }
}