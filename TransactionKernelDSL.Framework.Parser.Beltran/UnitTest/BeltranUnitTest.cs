using NUnit.Framework;
using TransactionKernelDSL.Framework.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Beltran.UnitTest
{
    [TestFixture]
    public class BeltranUnitTest
    {
        [Test]
        public void DisassembleEmisorOkTest()
        {
            BeltranParser testObject = new BeltranParser("Beltran", false);
            byte[] testArray = new byte[] { 0x30, 0x32, 0x30, 0x30, 0x7C, 0x30, 0x30, 0x33, 0x30, 0x30, 0x33, 0x7C, 0x30, 0x30, 0x30, 0x33, 0x30, 0x35, 0x30, 0x32, 0x7C, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x37, 0x33, 0x7C, 0x30, 0x30, 0x39, 0x31, 0x7C, 0x31, 0x7C, 0x32, 0x35, 0x31, 0x31, 0x32, 0x30, 0x31, 0x35, 0x30, 0x35, 0x30, 0x31, 0x35, 0x37, 0x7C, 0x34, 0x35, 0x7C, 0x33, 0x31, 0x33, 0x38, 0x39, 0x39, 0x30, 0x37, 0x38, 0x30, 0x7C, 0x32, 0x30, 0x30, 0x30 };
            Debug.WriteLine("Setting stream...");
            testObject.RequestStream.Set(testArray, testArray.Length);
            Debug.WriteLine("Disassembling...");
            Assert.True(testObject.Disassemble());
            Debug.WriteLine("Disassembled!");
        }

        [Test]
        public void GetOperationId_Test()
        {
            BeltranParser testObject = new BeltranParser("Beltran");
            byte[] testArray = new byte[] { 0x30, 0x32, 0x30, 0x30, 0x7C, 0x30, 0x30, 0x33, 0x30, 0x30, 0x33, 0x7C, 0x30, 0x30, 0x30, 0x33, 0x30, 0x35, 0x30, 0x32, 0x7C, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x37, 0x33, 0x7C, 0x30, 0x30, 0x39, 0x31, 0x7C, 0x31, 0x7C, 0x32, 0x35, 0x31, 0x31, 0x32, 0x30, 0x31, 0x35, 0x30, 0x35, 0x30, 0x31, 0x35, 0x37, 0x7C, 0x34, 0x35, 0x7C, 0x33, 0x31, 0x33, 0x38, 0x39, 0x39, 0x30, 0x37, 0x38, 0x30, 0x7C, 0x32, 0x30, 0x30, 0x30 };
            testObject.RequestStream.Set(testArray, testArray.Length);

            Assert.True(testObject.Disassemble(),testObject.ErrorMessage);
            //Debug.WriteLine(testObject.RequestStructure[0]);
            //Debug.WriteLine(testObject.RequestStructure[1]);
            //Debug.WriteLine(testObject.RequestStructure.GetOperationId());
            Assert.AreEqual("0200003003", testObject.RequestStructure.GetOperationId());
            //Debug.WriteLine(testObject.RequestStream.ToString());
        }

        [Test]
        public void AssembleRequestStructureTest()
        {
            BeltranParser testObject = new BeltranParser("Beltran", false);
            BeltranStructure testStructure = new BeltranStructure(AbstractTransactionParserStructureType.Request);

            ((BeltranField)testStructure["0"]).Content = "0200";
            ((BeltranField)testStructure["1"]).Content = "003003";
            ((BeltranField)testStructure["2"]).Content = "00030502";
            ((BeltranField)testStructure["3"]).Content = "00000173";
            ((BeltranField)testStructure["4"]).Content = "0091";
            ((BeltranField)testStructure["5"]).Content = "1";
            ((BeltranField)testStructure["6"]).Content = "25112015050157";
            ((BeltranField)testStructure["7"]).Content = "45";
            ((BeltranField)testStructure["8"]).Content = "3138990780";
            ((BeltranField)testStructure["9"]).Content = "2000";

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Debug.WriteLine((testObject.ResponseStream as BeltranStream).ToString());
            byte[] expectedResult = new byte[] { 0x30, 0x32, 0x30, 0x30, 0x7C, 0x30, 0x30, 0x33, 0x30, 0x30, 0x33, 0x7C, 0x30, 0x30, 0x30, 0x33, 0x30, 0x35, 0x30, 0x32, 0x7C, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x37, 0x33, 0x7C, 0x30, 0x30, 0x39, 0x31, 0x7C, 0x31, 0x7C, 0x32, 0x35, 0x31, 0x31, 0x32, 0x30, 0x31, 0x35, 0x30, 0x35, 0x30, 0x31, 0x35, 0x37, 0x7C, 0x34, 0x35, 0x7C, 0x33, 0x31, 0x33, 0x38, 0x39, 0x39, 0x30, 0x37, 0x38, 0x30, 0x7C, 0x32, 0x30, 0x30, 0x30 };

            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], testObject.ResponseStream[i]);
            }
        }

        [Test]
        public void AssembleResponseStructureTest()
        {
            BeltranParser testObject = new BeltranParser("Beltran");
            BeltranStructure testStructure = new BeltranStructure(AbstractTransactionParserStructureType.Response);

            ((BeltranField)testStructure["0"]).Content = "0200";
            ((BeltranField)testStructure["1"]).Content = "003003";
            ((BeltranField)testStructure["2"]).Content = "00030502";
            ((BeltranField)testStructure["3"]).Content = "00000173";
            ((BeltranField)testStructure["4"]).Content = "0091";
            ((BeltranField)testStructure["5"]).Content = "1";
            ((BeltranField)testStructure["6"]).Content = "25112015050157";
            ((BeltranField)testStructure["7"]).Content = "45";
            ((BeltranField)testStructure["8"]).Content = "3138990780";
            ((BeltranField)testStructure["9"]).Content = "2000";

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Debug.WriteLine(testObject.ResponseStream.ToString());

            byte[] expectedResult = new byte[] { 0x30, 0x32, 0x30, 0x30, 0x7C, 0x30, 0x30, 0x33, 0x30, 0x30, 0x33, 0x7C, 0x30, 0x30, 0x30, 0x33, 0x30, 0x35, 0x30, 0x32, 0x7C, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x37, 0x33, 0x7C, 0x30, 0x30, 0x39, 0x31, 0x7C, 0x31, 0x7C, 0x32, 0x35, 0x31, 0x31, 0x32, 0x30, 0x31, 0x35, 0x30, 0x35, 0x30, 0x31, 0x35, 0x37, 0x7C, 0x34, 0x35, 0x7C, 0x33, 0x31, 0x33, 0x38, 0x39, 0x39, 0x30, 0x37, 0x38, 0x30, 0x7C, 0x32, 0x30, 0x30, 0x30 };

            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], testObject.ResponseStream[i]);
            }
        }
    }
}
//"0200|003003|00030502|00000173|0091|1|25112015050157|45|3138990780|2000"
//tipomensaje (04)
//tipo procesamiento (06)
//comercio id (08)
//terminal id (08)
//clave de venta (04)
//transaccion id (05) ciclo diario
//DDmmYYYYHH (14)
//codigo operador recarga (02) 
//numero de telefono (10)
//monto (06)