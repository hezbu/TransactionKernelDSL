using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser.Tests
{
    [TestClass]
    public class BPosBrowserUnitTest
    {
        [TestMethod]
        public void CanDisassembleTest()
        {
            string test = "GA0142<T_MSG IMEI=\"863771020483204\" IMSI=\"722310112055484\" SN=\"ABCD1234\" TRX=\"1\" MsgType=\"L\"><InputValue VER=\"V160509G3.0_35\" /></T_MSG>";

            BPosBrowserParser testObject = new BPosBrowserParser();
            (testObject.RequestStream as BPosBrowserStream).Set(test);

            if (testObject.Disassemble() == false)
            {
                Assert.Fail("Error en Disassemble -> " + testObject.ErrorMessage);
            }
        }

        [TestMethod]
        public void RecognizeOperationTest()
        {
            string test = "GA0142<T_MSG IMEI=\"863771020483204\" IMSI=\"722310112055484\" SN=\"ABCD1234\" TRX=\"1\" MsgType=\"L\"><InputValue VER=\"V160509G3.0_35\" /></T_MSG>";

            BPosBrowserParser testObject = new BPosBrowserParser();
            (testObject.RequestStream as BPosBrowserStream).Set(test);

            if (testObject.Disassemble() == false)
            {
                Assert.Fail("Error en Disassemble -> " + testObject.ErrorMessage);
            }

            Assert.IsTrue(testObject.RequestStructure.GetOperationId().ToString() == "L");
        }

        [TestMethod]
        public void CanAssembleTest()
        {
            /*
             * <S_MSG Fprint="%%d%%cBIENVENIDO/r%%n%%c09/05/2016  14:19:40/r%%cID:5026-6249 /r/r" Date="09052016" Time="141800" PollRing="3" Banner="Activo de 09:00h a 14:15h" BottomLine="Ultima conexion 12:04:40" FastDial="REPORTE DIARIO : 1+OK" T0kbd="240" T0Rx="20" Poll="2" PollStart="0900" PollStop="1419"></S_MSG>
             */

            string test = "<S_MSG Fprint=\"%%d%%cBIENVENIDO/r%%n%%c09/05/2016  14:19:40/r%%cID:5026-6249 /r/r\" Date=\"09052016\" Time=\"141800\" PollRing=\"3\" Banner=\"Activo de 09:00h a 14:15h\" BottomLine=\"Ultima conexion 12:04:40\" FastDial=\"REPORTE DIARIO : 1+OK\" T0kbd=\"240\" T0Rx=\"20\" Poll=\"2\" PollStart=\"0900\" PollStop=\"1419\"></S_MSG>";
            test = String.Format("GA{0:D4}{1}", test.Length, test);

            BPosBrowserParser testObject = new BPosBrowserParser();
            BPosBrowserStructure str = testObject.ResponseStructure as BPosBrowserStructure;

            str.Main.Add("Fprint", "%%d%%cBIENVENIDO/r%%n%%c09/05/2016  14:19:40/r%%cID:5026-6249 /r/r");
            str.Main.Add("Date", "09052016");
            str.Main.Add("Time", "141800");
            str.Main.Add("PollRing", "3");
            str.Main.Add("Banner", "Activo de 09:00h a 14:15h");
            str.Main.Add("BottomLine", "Ultima conexion 12:04:40");
            str.Main.Add("FastDial", "REPORTE DIARIO : 1+OK");
            str.Main.Add("T0kbd", "240");
            str.Main.Add("T0Rx", "20");
            str.Main.Add("Poll", "2");
            str.Main.Add("PollStart", "0900");
            str.Main.Add("PollStop", "1419");

            testObject.ResponseStructure = str;


            if (testObject.Assemble() == false)
            {
                Assert.Fail("Error en Assemble -> " + testObject.ErrorMessage);
            }

            Assert.AreEqual(test, (testObject.ResponseStream as BPosBrowserStream).Get());
        }
    }
}

