namespace IntrepidProducts.IocContainer.Tests.TestObjects
{
    public interface ITransientTest
    {
        string TestProperty { get; set; }
    }

    public class TransientTestObject : ITransientTest
    {
        public string TestProperty { get; set; }
    }

    public class TransientTestObject2 : ITransientTest
    {
        public string TestProperty { get; set; }
    }
}