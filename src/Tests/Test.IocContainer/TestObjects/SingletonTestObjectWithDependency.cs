namespace IntrepidProducts.IocContainer.Tests.TestObjects
{
    public interface ISingletonObjectWithDependencyTest
    {
        string TestProperty { get; set; }
        ISingletonTestDependencyObject MyDependency { get; }
    }


    public class SingletonTestObjectWithDependency : ISingletonObjectWithDependencyTest
    {
        public SingletonTestObjectWithDependency(ISingletonTestDependencyObject dependency)
        {
            MyDependency = dependency;
        }

        public ISingletonTestDependencyObject MyDependency { get; private set; }
        public string TestProperty { get; set; }
    }
}