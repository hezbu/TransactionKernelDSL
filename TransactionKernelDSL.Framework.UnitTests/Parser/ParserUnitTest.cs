using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.UnitTests.Parser.Mocks;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Parser
{
    [TestClass]
    public class ParserUnitTest
    {
        [TestMethod]
        public void CanInstantiateParser()
        {
            ITransactionParser parser = new MockTransactionParser();
            Assert.IsNotNull(parser);
        }
    }
}
