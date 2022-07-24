using IntrepidProducts.IocContainer.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.MicrosoftStrategyTest
{
    [TestClass]
    public class MicrosoftStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new MicrosoftStrategy.MicrosoftStrategy());
        }
    }
}