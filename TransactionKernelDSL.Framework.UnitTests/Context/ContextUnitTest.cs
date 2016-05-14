using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.UnitTests.Context.Mocks;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Context
{
    [TestClass]
    public class ContextUnitTest
    {
        [TestMethod]
        public void CanInstantiateContext()
        {
            ITransactionContext context = new MockTransactionContext();
            Assert.IsNotNull(context);
        }

        [TestMethod]
        public void CanIterateItems()
        {
            ITransactionContext context = new MockTransactionContext();
            context["1"] = "a";
            context["2"] = "b";
            context["3"] = "c";

            foreach (var item in context)
            {
                Assert.IsTrue(item.Key.Contains("1") || item.Key.Contains("2") || item.Key.Contains("3"));
                Assert.IsTrue(item.Value.Contains("a") || item.Value.Contains("b") || item.Value.Contains("c"));
            }
        }
    }
}
