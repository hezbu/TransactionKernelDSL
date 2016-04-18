using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583.UnitTest
{
    [TestFixture]
    public class Iso8583UnitTest
    {
        [Test]
        public void DisassembleNewParser()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            //   byte[] test = new byte[] { 0x60, 0x00, 0x01, 0x00, 0x00, 0x02, 0x00, 0x32, 0x38, 0x01, 0x00, 0x00, 0xC1, 0x00, 0x1C, 0x00, 0x30, 0x03, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x27, 0x07, 0x20, 0x12, 0x11, 0x00, 0x00, 0x41, 0x11, 0x17, 0x07, 0x07, 0x27, 0x00, 0x01, 0x30, 0x30, 0x35, 0x31, 0x39, 0x39, 0x33, 0x31, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x00, 0x05, 0x30, 0x39, 0x39, 0x30, 0x37, 0x00, 0x23, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x31, 0x00, 0x25, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x38, 0x31, 0x39, 0x30, 0x37, 0x34, 0x31, 0x30, 0x45, 0x42, 0x41, 0x58, 0x45, 0x39, 0x39, 0x2E, 0x30, 0x31, 0x00, 0x12, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x41, 0x41 };
            byte[] test = new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x22, 0x38, 0x01, 0x00, 0x00, 0xC1, 0x10, 0x18, 0x00, 0x30, 0x03, 0x28, 0x12, 0x20, 0x12, 0x00, 0x00, 0x00, 0x33, 0x00, 0x00, 0x41, 0x12, 0x28, 0x00, 0x01, 0x32, 0x30, 0x31, 0x31, 0x32, 0x37, 0x39, 0x34, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x32, 0x34, 0x31, 0x33, 0x00, 0x09, 0x30, 0x32, 0x32, 0x30, 0x31, 0x30, 0x30, 0x30, 0x30, 0x30, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00, 0x19, 0x38, 0x39, 0x35, 0x32, 0x30, 0x32, 0x30, 0x34, 0x31, 0x31, 0x32, 0x39, 0x36, 0x30, 0x37, 0x39, 0x34, 0x35, 0x31, 0x00, 0x25, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x30, 0x31, 0x30, 0x31, 0x31, 0x33, 0x30, 0x38, 0x30, 0x53, 0x41, 0x32, 0x4D, 0x38, 0x30, 0x39, 0x30, 0x31 };
            #region Disassembling Test Stage

            testObject.RequestStream.Set(test);

            if (testObject.Disassemble() == false)
            {
                throw new AssertionException("Error en Disassemble -> " + testObject.ErrorMessage);
            }
            else
            {

                Debug.WriteLine("Field 61 -> SerieTerminal:" + testObject.RequestStructure["61"]["TerminalSerial"].ToString());
                Debug.WriteLine("Field 61 -> Marca:" + testObject.RequestStructure["61"]["Marca"].ToString());
                Debug.WriteLine("Field 61 -> Tipo:" + testObject.RequestStructure["61"]["Tipo"].ToString());
                Debug.WriteLine("Field 61 -> Version:" + testObject.RequestStructure["61"]["Version"].ToString());

                Debug.WriteLine("Field 61' -> SerieTerminal:" + testObject.RequestStructure["Datos de Terminal"]["TerminalSerial"].ToString());
                Debug.WriteLine("Field 61' -> Marca:" + testObject.RequestStructure["Datos de Terminal"]["Marca"].ToString());
                Debug.WriteLine("Field 61' -> Tipo:" + testObject.RequestStructure["Datos de Terminal"]["Tipo"].ToString());
                Debug.WriteLine("Field 61' -> Version:" + testObject.RequestStructure["Datos de Terminal"]["Version"].ToString());

                Debug.WriteLine("Field 7 -> Año:" + testObject.RequestStructure["7"]["Año"].ToString());
                Debug.WriteLine("Field 7 -> Mes:" + testObject.RequestStructure["7"]["Mes"].ToString());
                Debug.WriteLine("Field 7 -> Dia:" + testObject.RequestStructure["7"]["Dia"].ToString());
                Debug.WriteLine("Field 7 -> Hora:" + testObject.RequestStructure["7"]["Hora"].ToString());

                Debug.WriteLine("Field 48 -> 0:" + testObject.RequestStructure["48"][0].ToString());
                Debug.WriteLine("Field 48 -> 1:" + testObject.RequestStructure["48"][1].ToString());
                Debug.WriteLine("Field 48 -> 2:" + testObject.RequestStructure["48"][2].ToString());
                Debug.WriteLine("Field 48 -> 3:" + testObject.RequestStructure["48"][3].ToString());


            }
            #endregion

            #region Assembling Test Stage
            Iso8583Structure resp = testObject.ResponseStructure as Iso8583Structure;
            Iso8583Structure req = testObject.RequestStructure as Iso8583Structure;

            Buffer.BlockCopy(req["TPDU"].Content, 0, resp["TPDU"].Content, 0, resp["TPDU"].Content.Length - 1);

            Buffer.BlockCopy(req["MSGID"].Content, 0, resp["MSGID"].Content, 0, resp["MSGID"].Content.Length - 1);
            resp["MSGID"].Content[2] += 0x01;

            Buffer.BlockCopy(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 0, resp["BITMAP"].Content, 0, resp["BITMAP"].Content.Length - 1);

            resp.TurnOnField(3);
            Buffer.BlockCopy(req["3"].Content, 0, resp["3"].Content, 0, resp["3"].Content.Length - 1);

            string strField7 = System.DateTime.Now.Day.ToString().PadLeft(2, '0') + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + System.DateTime.Now.Year.ToString().PadLeft(4, '0') + System.DateTime.Now.Hour.ToString().PadLeft(2, '0') + System.DateTime.Now.Minute.ToString().PadLeft(2, '0') + System.DateTime.Now.Second.ToString().PadLeft(2, '0') + System.DateTime.Now.Millisecond.ToString().PadLeft(3, '0');
            resp.TurnOnField(7);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(strField7.Substring(0, 10)), 0, resp["7"].Content, 0, resp["7"].Content.Length - 1);

            resp.TurnOnField(11);
            Buffer.BlockCopy(req["11"].Content, 0, resp["11"].Content, 0, resp["11"].Content.Length - 1);

            resp.TurnOnField(12);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(strField7.Substring(8, 6)), 0, resp["12"].Content, 0, resp["12"].Content.Length - 1);

            resp.TurnOnField(13);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(strField7.Substring(2, 2) + strField7.Substring(0, 2)), 0, resp["13"].Content, 0, resp["13"].Content.Length - 1);

            resp.TurnOnField(24);
            Buffer.BlockCopy(req["24"].Content, 0, resp["24"].Content, 0, resp["24"].Content.Length - 1);

            resp.TurnOnField(39);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes("00"), 0, resp["39"].Content, 0, resp["39"].Content.Length - 1);

            resp.TurnOnField(41);
            Buffer.BlockCopy(req["41"].Content, 0, resp["41"].Content, 0, resp["41"].Content.Length - 1);

            resp.TurnOnField(42);
            Buffer.BlockCopy(req["42"].Content, 0, resp["42"].Content, 0, resp["42"].Content.Length - 1);

            resp.TurnOnField(54);
          
            Buffer.BlockCopy(AbstractTransactionFacade.GetBcdBytes("NVA".Length.ToString().PadLeft(4, '0')), 0, resp["54"].Content, 0, 2);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes("NVA"), 0, resp["54"].Content, 2, "NVA".Length);
            resp["54"].Length = 2 + "NVA".Length;

            
            string mensaje = "Mensaje de prueba; si, esto es una prueba";
            resp.TurnOnField(60);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBcdBytes(mensaje.Length.ToString().PadLeft(4, '0')), 0, resp["60"].Content, 0, 2);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(mensaje), 0, resp["60"].Content, 2, mensaje.Length);
            resp["60"].Length = 2 + mensaje.Length;


            mensaje = "15290".PadLeft(10, '0');
            resp.TurnOnField(63);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBcdBytes(mensaje.Length.ToString().PadLeft(4, '0')), 0, resp["63"].Content, 0, 2);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(mensaje), 0, resp["63"].Content, 2, mensaje.Length);
            resp["63"].Length = 2 + mensaje.Length;


            if (testObject.Assemble() == false)
            {
                throw new Exception("Error en Assemble -> " + testObject.ErrorMessage);
            }
            #endregion

        }



        [Test]
        public void DisassembleOnlyField3Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x99, 0x00, 0x00 }, 18);
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassemblePPMX0100003003_de_LlanosTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x22, 0x38, 0x01, 0x00, 0x00, 0xC1, 0x00, 0x08, 0x19, 0x02, 0x20, 0x13, 0x00, 0x00, 0x00, 0x01, 0x00, 0x02, 0x55, 0x02, 0x19, 0x00, 0x01, 0x32, 0x30, 0x31, 0x31, 0x32, 0x37, 0x39, 0x34, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x31, 0x32, 0x34, 0x31, 0x33, 0x00, 0x09, 0x30, 0x32, 0x32, 0x30, 0x31, 0x30, 0x30, 0x30, 0x30, 0x00, 0x25, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x30, 0x32, 0x30, 0x31, 0x31, 0x32, 0x37, 0x39, 0x34, 0x43, 0x50, 0x43, 0x50, 0x37, 0x30, 0x38, 0x31, 0x31 }, 91);
            Assert.AreEqual(false, testObject.Disassemble());
        }
        [Test]
        public void DisassembleOnlyBadBitmap1Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x08, 0x00, 0x22, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x99, 0x00, 0x00 }, 18);
            Assert.AreEqual(false, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField6Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x84, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00 }, 29);
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField2OddTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();

            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x17, 0x12, 0x34, 0x99, 0x88, 0x56, 0x78, 0x77, 0x66, 0x5F, 0x00, 0x30, 0x03 });

            Assert.AreEqual(true, testObject.Disassemble());
            Assert.AreEqual(true, testObject.RequestStructure["2"].TrueLength == 17);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[1] == 0x31);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[2] == 0x32);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[3] == 0x33);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[4] == 0x34);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[5] == 0x39);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[6] == 0x39);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[7] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[8] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[9] == 0x35);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[10] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[11] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[12] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[13] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[14] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[15] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[16] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[17] == 0x35);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[18] == 0x00);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[0] == 0x30);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[1] == 0x30);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[2] == 0x33);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[3] == 0x30);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[4] == 0x30);
            Assert.AreEqual(true, testObject.RequestStructure["3"].Content[5] == 0x33);
        }
        [Test]
        public void DisassembleField2EvenTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();

            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x16, 0x12, 0x34, 0x99, 0x88, 0x56, 0x78, 0x77, 0x66, 0x00, 0x30, 0x03 });

            Assert.AreEqual(true, testObject.Disassemble());
            Assert.AreEqual(true, testObject.RequestStructure["2"].TrueLength == 16);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[1] == 0x31);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[2] == 0x32);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[3] == 0x33);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[4] == 0x34);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[5] == 0x39);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[6] == 0x39);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[7] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[8] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[9] == 0x35);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[10] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[11] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[12] == 0x38);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[13] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[14] == 0x37);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[15] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[16] == 0x36);
            Assert.AreEqual(true, testObject.RequestStructure["2"].Content[17] == 0x00);
        }
        [Test]
        public void DisassembleField14Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 25);
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField22Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 25);
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField38Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField39Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField43Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField44Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void DisassembleField45Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            testObject.RequestStream.Set(new byte[] { 0x60, 0x00, 0x03, 0x00, 0x00, 0x02, 0x00, 0x80, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, });
            Assert.AreEqual(true, testObject.Disassemble());
        }
        [Test]
        public void AssembleLLVARField()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10 });
            testStructure["3"].CopyContentFrom(new byte[] { 0x99, 0x00, 0x00 });
            testStructure["4"].CopyContentFrom(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x45, 0x00 });
            testStructure["60"].CopyContentFrom(new byte[] { 0x00, 0x10, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31, 0x31 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());

            Assert.AreEqual(true, testObject.ResponseStream[17] == 0x99);
            Assert.AreEqual(true, testObject.ResponseStream[18] == 0x00);
            Assert.AreEqual(true, testObject.ResponseStream[19] == 0x00);

            Assert.AreEqual(true, (testObject.ResponseStream)[20] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[21] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[22] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[23] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[24] == 0x45);
            Assert.AreEqual(true, (testObject.ResponseStream)[25] == 0x00);

            Assert.AreEqual(true, (testObject.ResponseStream)[26] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[27] == 0x10);
            Assert.AreEqual(true, (testObject.ResponseStream)[28] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[29] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[30] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[31] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[32] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[33] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[34] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[35] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[36] == 0x31);
            Assert.AreEqual(true, (testObject.ResponseStream)[37] == 0x31);
        }
        [Test]
        public void AssembleOkTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["3"].CopyContentFrom(new byte[] { 0x99, 0x00, 0x00 });
            testStructure["4"].CopyContentFrom(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x45, 0x00 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
        }
        [Test]
        public void AssembleOkBadMsgIdTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].Content = null;
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["3"].CopyContentFrom(new byte[] { 0x99, 0x00, 0x00 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(false, testObject.Assemble());

        }
        [Test]
        public void AssembleOkBadTPDUTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].Content = null;
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["3"].CopyContentFrom(new byte[] { 0x99, 0x00, 0x00 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(false, testObject.Assemble());
        }
        [Test]
        public void AssembleOkBadBitmapTest()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].Content = null;
            testStructure["3"].CopyContentFrom(new byte[] { 0x99, 0x00, 0x00 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(false, testObject.Assemble());
        }
        [Test]
        public void Assemble_OddField2Test()
        {
            #region Prueba #1 - Longitud 17
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["2"].CopyContentFrom(new byte[] { 0x17, 0x12, 0x34, 0x99, 0x88, 0x12, 0x34, 0x55, 0x67, 0x3F });
            testStructure["3"].CopyContentFrom(new byte[] { 0x00, 0x30, 0x03 });

            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Assert.AreEqual(true, (testObject.ResponseStream)[17] == 0x17);
            Assert.AreEqual(true, (testObject.ResponseStream)[18] == 0x12);
            Assert.AreEqual(true, (testObject.ResponseStream)[19] == 0x34);
            Assert.AreEqual(true, (testObject.ResponseStream)[20] == 0x99);
            Assert.AreEqual(true, (testObject.ResponseStream)[21] == 0x88);
            Assert.AreEqual(true, (testObject.ResponseStream)[22] == 0x12);
            Assert.AreEqual(true, (testObject.ResponseStream)[23] == 0x34);
            Assert.AreEqual(true, (testObject.ResponseStream)[24] == 0x55);
            Assert.AreEqual(true, (testObject.ResponseStream)[25] == 0x67);
            Assert.AreEqual(true, (testObject.ResponseStream)[26] == 0x3F);
            Assert.AreEqual(true, (testObject.ResponseStream)[27] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[28] == 0x30);
            Assert.AreEqual(true, (testObject.ResponseStream)[29] == 0x03);

            #endregion
            #region Prueba #2 - Longitud 21
            testObject = new Iso8583Parser();
            testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["2"].CopyContentFrom(new byte[] { 0x21, 0x12, 0x34, 0x99, 0x88, 0x12, 0x34, 0x55, 0x67, 0x32, 0x34, 0x5F });
            testStructure["3"].CopyContentFrom(new byte[] { 0x00, 0x30, 0x03 });


            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Assert.AreEqual(true, (testObject.ResponseStream)[17] == 0x21);
            Assert.AreEqual(true, (testObject.ResponseStream)[18] == 0x12);
            Assert.AreEqual(true, (testObject.ResponseStream)[19] == 0x34);
            Assert.AreEqual(true, (testObject.ResponseStream)[20] == 0x99);
            Assert.AreEqual(true, (testObject.ResponseStream)[21] == 0x88);
            Assert.AreEqual(true, (testObject.ResponseStream)[22] == 0x12);
            Assert.AreEqual(true, (testObject.ResponseStream)[23] == 0x34);
            Assert.AreEqual(true, (testObject.ResponseStream)[24] == 0x55);
            Assert.AreEqual(true, (testObject.ResponseStream)[25] == 0x67);
            Assert.AreEqual(true, (testObject.ResponseStream)[26] == 0x32);
            Assert.AreEqual(true, (testObject.ResponseStream)[27] == 0x34);
            Assert.AreEqual(true, (testObject.ResponseStream)[28] == 0x5F);
            Assert.AreEqual(true, (testObject.ResponseStream)[29] == 0x00);
            Assert.AreEqual(true, (testObject.ResponseStream)[30] == 0x30);
            Assert.AreEqual(true, (testObject.ResponseStream)[31] == 0x03);
            #endregion

        }
        [Test]
        public void Assemble_EvenField2Test()
        {

            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Response);

            testStructure["TPDU"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00 });
            testStructure["MSGID"].CopyContentFrom(new byte[] { 0x08, 0x10 });
            testStructure["BITMAP"].CopyContentFrom(new byte[] { 0x60, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });
            testStructure["2"].CopyContentFrom(new byte[] { 0x16, 0x12, 0x34, 0x99, 0x88, 0x12, 0x34, 0x55, 0x67 });
            testStructure["3"].CopyContentFrom(new byte[] { 0x00, 0x30, 0x03 });


            testObject.ResponseStructure = testStructure;
            Assert.AreEqual(true, testObject.Assemble());
            Assert.AreEqual(true, testObject.ResponseStream[17] == 0x16);
            Assert.AreEqual(true, testObject.ResponseStream[18] == 0x12);
            Assert.AreEqual(true, testObject.ResponseStream[19] == 0x34);
            Assert.AreEqual(true, testObject.ResponseStream[20] == 0x99);
            Assert.AreEqual(true, testObject.ResponseStream[21] == 0x88);
            Assert.AreEqual(true, testObject.ResponseStream[22] == 0x12);
            Assert.AreEqual(true, testObject.ResponseStream[23] == 0x34);
            Assert.AreEqual(true, testObject.ResponseStream[24] == 0x55);
            Assert.AreEqual(true, testObject.ResponseStream[25] == 0x67);
            Assert.AreEqual(true, testObject.ResponseStream[26] == 0x00);
            Assert.AreEqual(true, testObject.ResponseStream[27] == 0x30);
            Assert.AreEqual(true, testObject.ResponseStream[28] == 0x03);

        }
        [Test]
        public void Assemble_Field35Test()
        {
            Iso8583Parser testObject = new Iso8583Parser();
            Iso8583Parser testObject_Take2 = new Iso8583Parser();

            byte[] testField35 = new byte[] {   0x60, 0x00, 0x00, 0x00, 0x00,
                                                0x02, 0x00, 
                                                0x20, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00,
                                                0x00, 0x20, 0x00,
                                                0X32, 0x60,0x36,0x04,0x12,0x12,0x51,0x67,0x07,0xD1,0x21,0x55,0x01,0x00,0x00,0x00,0x00};

            byte[] testField35_Take2 = new byte[] {   0x60, 0x00, 0x00, 0x00, 0x00,
                                                0x02, 0x00, 
                                                0x20, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00,
                                                0x00, 0x20, 0x00,
                                                0X33, 0x60,0x36,0x04,0x12,0x12,0x51,0x67,0x07,0x9D,0x12,0x15, 0x50, 0x10,0x00,0x00,0x00,0x00,0x00};

            testObject.RequestStream.Set(testField35);

            Assert.AreEqual(true, testObject.Disassemble());

            Assert.AreEqual(testObject.RequestStructure["35"].Content[0], 0x32);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[1], 0x36);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[2], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[3], 0x33);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[4], 0x36);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[5], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[6], 0x34);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[7], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[8], 0x32);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[9], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[10], 0x32);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[11], 0x35);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[12], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[13], 0x36);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[14], 0x37);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[15], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[16], 0x37);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[17], 0x3D);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[18], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[19], 0x32);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[20], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[21], 0x35);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[22], 0x35);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[23], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[24], 0x31);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[25], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[26], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[27], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[28], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[29], 0x30);
            Assert.AreEqual(testObject.RequestStructure["35"].Content[30], 0x30);


            testObject_Take2.RequestStream.Set(testField35_Take2);

            Assert.AreEqual(true, testObject_Take2.Disassemble());

            //33 36 30 33 
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[0], 0x33);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[1], 0x36);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[2], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[3], 0x33);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[4], 0x36);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[5], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[6], 0x34);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[7], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[8], 0x32);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[9], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[10], 0x32);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[11], 0x35);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[12], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[13], 0x36);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[14], 0x37);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[15], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[16], 0x37);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[17], 0x39);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[18], 0x3D);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[19], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[20], 0x32);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[21], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[22], 0x35);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[23], 0x35);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[24], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[25], 0x31);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[26], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[27], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[28], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[29], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[30], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[31], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[32], 0x30);
            Assert.AreEqual(testObject_Take2.RequestStructure["35"].Content[33], 0x30);

        }
        [Test]
        public void TestLlvarLengthBuildTest()
        {
            byte[] expectedResult = new byte[] { 0x99, 0x99 };
            Assert.AreEqual(expectedResult[0], AbstractTransactionFacade.GetBcdBytes("9999")[0]);
            Assert.AreEqual(expectedResult[1], AbstractTransactionFacade.GetBcdBytes("9999")[1]);

            expectedResult = new byte[] { 0x09, 0x99 };
            Assert.AreEqual(expectedResult[0], AbstractTransactionFacade.GetBcdBytes("0999")[0]);
            Assert.AreEqual(expectedResult[1], AbstractTransactionFacade.GetBcdBytes("0999")[1]);

            expectedResult = new byte[] { 0x00, 0x99 };
            Assert.AreEqual(expectedResult[0], AbstractTransactionFacade.GetBcdBytes("0099")[0]);
            Assert.AreEqual(expectedResult[1], AbstractTransactionFacade.GetBcdBytes("0099")[1]);

            expectedResult = new byte[] { 0x00, 0x09 };
            Assert.AreEqual(expectedResult[0], AbstractTransactionFacade.GetBcdBytes("0009")[0]);
            Assert.AreEqual(expectedResult[1], AbstractTransactionFacade.GetBcdBytes("0009")[1]);

            expectedResult = new byte[] { 0x01, 0x00 };
            Assert.AreEqual(expectedResult[0], AbstractTransactionFacade.GetBcdBytes("0100")[0]);
            Assert.AreEqual(expectedResult[1], AbstractTransactionFacade.GetBcdBytes("0100")[1]);

        }
    }
}
