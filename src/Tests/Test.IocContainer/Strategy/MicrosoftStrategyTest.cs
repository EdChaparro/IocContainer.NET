using IntrepidProducts.IocContainer.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests.Strategy
{
    [TestClass]
    public class MicrosoftStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new MicrosoftStrategy());
        }
    }
}