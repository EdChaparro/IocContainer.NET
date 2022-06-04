namespace IntrepidProducts.IocContainer
{
    public abstract class IocFactoryAbstract 
    {
        protected IocFactoryAbstract(IIocContainer iocContainer)
        {
            IocContainer = iocContainer;
        }

        protected static IIocContainer IocContainer;

        public abstract void RegisterClasses();

        public static IIocContainer GetContainer()
        {
            return IocContainer;          
        }
    }
}