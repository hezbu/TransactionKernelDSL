using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.UnitTests.Engines.Mocks;
using TransactionKernelDSL.Framework.V2.BaseClasses;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Engines
{
    [TestClass]
    public class EnginesUnitTest
    {
        [TestMethod]
        public void CanInstantiateInterfacedEngine()
        {
            ITransactionEngine engine = MockInterfacedEngine.Factory.Build();


            Assert.AreEqual(engine.Start(), true);
            Assert.AreEqual(engine.IsEngineOn, true);
            Assert.AreEqual(engine.Stop(), true);
            Assert.AreEqual(engine.IsEngineOn, false);
        }

        [TestMethod]
        public void CanInstantiateMockBasedEngine()
        {
            ITransactionEngine engine = MockBasedEngine.Factory.Build<MockBasedEngine>();


            Assert.AreEqual(engine.GetType(), typeof(MockBasedEngine));
            Assert.AreEqual(engine.Start(), true);
            Assert.AreEqual(engine.IsEngineOn, true);
            Assert.AreEqual(engine.Stop(), true);
            Assert.AreEqual(engine.IsEngineOn, false);
        }
    }
}
