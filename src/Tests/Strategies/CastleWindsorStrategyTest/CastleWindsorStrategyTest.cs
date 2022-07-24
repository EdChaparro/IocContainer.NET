using IntrepidProducts.IocContainer.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IoC.CastleWindsorStrategyTest
{
    [TestClass]
    public class CastleWindsorStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new CastleWindsorStrategy.CastleWindsorStrategy());
        }
    }
}