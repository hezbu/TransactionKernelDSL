using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Json.Tests
{
    [TestClass]
    public class JsonParserUnitTest
    {
        [TestMethod]
        public void AssembleMethodTest()
        {
            var parser = new JsonParser();
            var rsp = new JsonParserStructure(AbstractTransactionParserStructureType.Response);
            rsp.MerchantId = "Merch1234";
            rsp.TerminalId = "Term1234";
            rsp.OperationId = "A";

            parser.ResponseStructure = rsp;
            Assert.IsTrue(parser.Assemble());
            
        }

        [TestMethod]
        public void AssembleMethodReturnsExpectedStringTest()
        {
            var parser = new JsonParser();
            var rsp = new JsonParserStructure(AbstractTransactionParserStructureType.Response);
            rsp.MerchantId = "Merch1234";
            rsp.TerminalId = "Term1234";
            rsp.OperationId = "A";

            parser.ResponseStructure = rsp;
            parser.Assemble();
            var output = (parser.ResponseStream as JsonParserStream).Get();
            var stub = "{\"OperationId\":\"A\",\"MerchantId\":\"Merch1234\",\"TerminalId\":\"Term1234\",\"Items\":[]}";
            Assert.IsTrue(output == String.Format("{0:D4}{1}",stub.Length,stub));

        }

        [TestMethod]
        public void DissassembleMethodTest()
        {
            var parser = new JsonParser();
            var rsp = new JsonParserStructure(AbstractTransactionParserStructureType.Response);
            rsp.MerchantId = "Merch1234";
            rsp.TerminalId = "Term1234";
            rsp.OperationId = "A";

            parser.ResponseStructure = rsp;
            parser.Assemble();
            var output = (parser.ResponseStream as JsonParserStream).Get();
            (parser.RequestStream as JsonParserStream).Set(output);
            Assert.IsTrue(parser.Disassemble());

        }

        [TestMethod]
        public void DissassembleMethodGetsFieldsSuccessfullyTest()
        {
            var parser = new JsonParser();
            var rsp = new JsonParserStructure(AbstractTransactionParserStructureType.Response);
            rsp.MerchantId = "Merch1234";
            rsp.TerminalId = "Term1234";
            rsp.OperationId = "A";

            parser.ResponseStructure = rsp;
            parser.Assemble();
            var output = (parser.ResponseStream as JsonParserStream).Get();
            (parser.RequestStream as JsonParserStream).Set(output);
            parser.Disassemble();
            var req = (parser.RequestStructure as JsonParserStructure);
            Assert.IsTrue(
                rsp.MerchantId == req.MerchantId &&
                rsp.OperationId == req.OperationId &&
                rsp.Items.Count == req.Items.Count &&
                rsp.TerminalId == req.TerminalId
                );

        }
    }
}

