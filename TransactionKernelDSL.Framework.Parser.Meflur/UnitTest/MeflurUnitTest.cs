using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TransactionKernelDSL.Framework.V1;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.Parser.Meflur.UnitTest
{
    [TestFixture]
    public class MeflurUnitTest
    {
        [Test]
        public void AssembleRequestStructureTest()
        {
            MeflurParser testObject = new MeflurParser("Meflur",false);
            MeflurStructure testStructure = new MeflurStructure(AbstractTransactionParserStructureType.Request);

            ((MeflurField)testStructure["Tipo"]).Content = "H";
            ((MeflurField)testStructure["Version"]).Content = "03";
            ((MeflurField)testStructure["Fecha"]).Content = "20120101";
            ((MeflurField)testStructure["Hora"]).Content = "161520000";
            ((MeflurField)testStructure["Comercio"]).Content = "12345678";
            ((MeflurField)testStructure["Terminal"]).Content = "12345678";
            ((MeflurField)testStructure["Transaccion"]).Content = "S";
            ((MeflurField)testStructure["ResultadoAnterior"]).Content = "0";
            ((MeflurField)testStructure["1"]).Content = "00000000";


            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Debug.WriteLine(testObject.ResponseStream.ToString());
            byte[] expectedResult = new byte[] { 0x02, 0x48, 0x30, 0x33, 0x32, 0x30, 0x31, 0x32, 0x30, 0x31, 0x30, 0x31, 0x31, 0x36, 0x31, 0x35, 0x32, 0x30, 0x30, 0x30, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x53, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03 };

            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], testObject.ResponseStream[i]);
            }

        }

        [Test]
        public void AssembleResponseStructureTest()
        {
            MeflurParser testObject = new MeflurParser("Meflur");
            MeflurStructure testStructure = new MeflurStructure(AbstractTransactionParserStructureType.Response);

            ((MeflurField)testStructure["Transaccion"]).Content = "E";
            ((MeflurField)testStructure["1"]).Content = "HS1020";
            ((MeflurField)testStructure["2"]).Content = "Mensaje de Error #1";
            ((MeflurField)testStructure["3"]).Content = "Mensaje de Error #2";
            ((MeflurField)testStructure["4"]).Content = "Mensaje de Error #1";
            ((MeflurField)testStructure["5"]).Content = " 00000000";

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Debug.WriteLine(testObject.ResponseStream.ToString());

            byte[] expectedResult = new byte[] { 0x02, 0x45, 0x48, 0x53, 0x31, 0x30, 0x32, 0x30, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x31, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x32, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x31, 0x04, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03 };

            for (int i = 0; i < expectedResult.Length; i++)
            {
                Assert.AreEqual(expectedResult[i], testObject.ResponseStream[i]);
            }
        }

        [Test]
        public void DisassembleReceptorOkTest()
        {
            //(02)H032012...

            MeflurParser testObject = new MeflurParser("Meflur");
            byte[] testArray = new byte[] { 0x02, 0x48, 0x30, 0x33, 0x32, 0x30, 0x31, 0x32, 0x30, 0x31, 0x30, 0x31, 0x31, 0x36, 0x31, 0x35, 0x32, 0x30, 0x30, 0x30, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x53, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03 };
            testObject.RequestStream.Set(testArray, testArray.Length);

            Assert.True(testObject.Disassemble());
            Debug.WriteLine(testObject.RequestStream.ToString());
            Debug.WriteLine("Subfield: " + ((MeflurSubfield)testObject.RequestStructure["Fecha"]["Año"]).Content);
        }

        [Test]
        public void DisassembleEmisorOkTest()
        {
            //(02)EHS...

            MeflurParser testObject = new MeflurParser("Meflur", false);
            byte[] testArray = new byte[] { 0x02, 0x45, 0x48, 0x53, 0x31, 0x30, 0x32, 0x30, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x31, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x32, 0x04, 0x4D, 0x65, 0x6E, 0x73, 0x61, 0x6A, 0x65, 0x20, 0x64, 0x65, 0x20, 0x45, 0x72, 0x72, 0x6F, 0x72, 0x20, 0x23, 0x31, 0x04, 0x20, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x03 };
            testObject.RequestStream.Set(testArray, testArray.Length);

            Assert.True(testObject.Disassemble());
        }
    }
}
