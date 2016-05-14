using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.UnitTests.Handler.Mocks;
using TransactionKernelDSL.Framework.V2.BaseClasses;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Handler
{
    [TestClass]
    public class HandlerUnitTest
    {
        [TestMethod]
        public void CanInstantiateHandler()
        {
            ITransactionHandler handler = new MockTransactionHandler(
                                                new MockTransactionContext(),
                                                new MockTransactionParser(),
                                                null
                                                );
            Assert.IsNotNull(handler);
        }

        [TestMethod]
        public void HandlerHasValidIdentifier()
        {
            ITransactionHandler handler = new MockTransactionHandler(
                                                new MockTransactionContext(),
                                                new MockTransactionParser(),
                                                null
                                                );

            Assert.AreEqual(handler.GetTransactionId(), "Mock");
        }
    }
}
