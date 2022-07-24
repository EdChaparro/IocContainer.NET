namespace IntrepidProducts.IocContainer.Tests.TestObjects
{
    public interface IIocTestSingleton
    {
        string TestProperty { get; set; }
    }

    public class SingletonTestObject : IIocTestSingleton
    {
        public string TestProperty { get; set; }
    }
}