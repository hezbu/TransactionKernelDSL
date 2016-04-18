using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.UnitTests.Facade.Mocks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.UnitTests.Facade
{
    [TestClass]
    public class FacadeUnitTest
    {
        [TestMethod]
        public void CanInstantiateMockFacade()
        {
            ITransactionFacade facade = new MockFacade();
            
            Assert.AreEqual(facade.InstanceId, 99);
            Assert.AreEqual(facade.StartEngines(), true);
            Assert.AreEqual(facade.StopEngines(), true);

        }
    }
}
