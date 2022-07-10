﻿using IntrepidProducts.IocContainer.Strategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests.Strategy
{
    [TestClass]
    public class AutofacStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new AutofacStrategy());
            IocTestFactory.RegisterClasses();
        }
    }
}