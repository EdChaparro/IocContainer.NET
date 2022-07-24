using IntrepidProducts.IocContainer.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.AutofacStrategyTest
{
    [TestClass]
    public class AutofacStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new AutofacStrategy.AutofacStrategy());
        }
    }
}