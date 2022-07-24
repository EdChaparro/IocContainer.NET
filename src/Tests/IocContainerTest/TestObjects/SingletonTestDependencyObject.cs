namespace IntrepidProducts.IocContainer.Tests.TestObjects
{
    public interface ISingletonTestDependencyObject
    {
        string TestProperty { get; set; }
    }

    public class SingletonTestDependencyObject : ISingletonTestDependencyObject
    {
        public string TestProperty { get; set; }
    }
}