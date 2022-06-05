﻿using Castle.MicroKernel;
using IntrepidProducts.IocContainer.Strategy;
using IntrepidProducts.IocContainer.Tests.TestObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IntrepidProducts.IocContainer.Tests.Strategy
{
    [TestClass]
    public class CastleWindsorStrategyTest : StrategyTestAbstract
    {
        [TestInitialize]
        public void Init()
        {
            IocTestFactory = new IocTestFactory(new CastleWindsorStrategy());
            IocTestFactory.RegisterClasses();
        }
    }
}