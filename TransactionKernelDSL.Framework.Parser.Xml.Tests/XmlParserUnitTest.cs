using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransactionKernelDSL.Framework.V1;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TransactionKernelDSL.Framework.Parser.Xml.Tests
{
    [TestClass]
    public class XmlParserUnitTest
    {
        [TestMethod]
        public void AssembleXmlParser_Test()
        {
            var parser = new XmlParser();
            var rsp = new XmlResponseStructure();

            rsp.CID = "00000000";
            rsp.TID = "4D790754";
            rsp.MTI = "3800";
            rsp.PCODE = "000000";
            rsp.VAPP = "1.10.99PA";
            rsp.TRC = "0";
            rsp.LTE = "1";
            rsp.COP = "001";
            rsp.DTMS = "20160624T220139";
           

            parser.ResponseStructure = rsp;
            Assert.IsTrue(parser.Assemble());
        }

        [TestMethod]
        public void Check3800SerializedDataIsCorrect_Test()
        {
            var parser = new XmlParser();

            (parser.RequestStream as XmlStream).Set("<?xml version=\"1.0\"?><T_MSG MTI=\"3800\" PCODE=\"000000\" TID=\"4D790752\" CID=\"00000000\" NID=\"4D790752\" VAPP=\"1.10.99PA\" TRC=\"0\" LTE=\"1\" COP=\"001\" DTMP=\"20160628T011823\"><MT3800000000 /></T_MSG>");
            parser.DisassembleMethod();
            var req = parser.RequestStructure as XmlRequestStructure;
            

            Assert.IsTrue(req.Root.T_MSG.MTI == "3800");
            Assert.IsTrue(req.Root.T_MSG.PCODE == "000000");
            Assert.IsTrue(req.Root.T_MSG.TID == "4D790752");
            Assert.IsTrue(req.Root.T_MSG.CID == "00000000");
            Assert.IsTrue(req.Root.T_MSG.NID == "4D790752");
            Assert.IsTrue(req.Root.T_MSG.VAPP == "1.10.99PA");
            Assert.IsTrue(req.Root.T_MSG.TRC == "0");
            Assert.IsTrue(req.Root.T_MSG.LTE == "1");
            Assert.IsTrue(req.Root.T_MSG.COP == "001");
            Assert.IsTrue(req.Root.T_MSG.DTMP == "20160628T011823");
            
        }



        [TestMethod]
        public void Check3200SerializedDataIsCorrect_Test()
        {
            var parser = new XmlParser();

            (parser.RequestStream as XmlStream).Set("<?xml version=\"1.0\"?><T_MSG MTI=\"3200\" PCODE=\"000000\" TID=\"4D790752\" CID=\"00000000\" NID=\"4D790752\" VAPP=\"1.10.99PA\" TRC=\"1\" LTE=\"1\" COP=\"001\" DTMP=\"20160627T142044\"><MT3200000000 PAN=\"6371170100894749\" TOTAL=\"18485\" TICKET=\"111\" PAY_METH=\"0\"><PRODUCTOS /></MT3200000000></T_MSG>");
            parser.DisassembleMethod();

            var req = parser.RequestStructure as XmlRequestStructure;        

            Assert.IsTrue(req.Root.T_MSG.MTI == "3200");
            Assert.IsTrue(req.Root.T_MSG.PCODE == "000000");
            Assert.IsTrue(req.Root.T_MSG.TID == "4D790752");
            Assert.IsTrue(req.Root.T_MSG.CID == "00000000");
            Assert.IsTrue(req.Root.T_MSG.NID == "4D790752");
            Assert.IsTrue(req.Root.T_MSG.VAPP == "1.10.99PA");
            Assert.IsTrue(req.Root.T_MSG.TRC == "1");
            Assert.IsTrue(req.Root.T_MSG.LTE == "1");
            Assert.IsTrue(req.Root.T_MSG.COP == "001");
            Assert.IsTrue(req.Root.T_MSG.DTMP == "20160627T142044");
            Assert.IsTrue(req.Root.T_MSG.MT3200000000.PAN == "6371170100894749");
            Assert.IsTrue(req.Root.T_MSG.MT3200000000.TOTAL == "18485");
            Assert.IsTrue(req.Root.T_MSG.MT3200000000.TICKET == "111");
            Assert.IsTrue(req.Root.T_MSG.MT3200000000.PAY_METH == "0");
            Assert.IsTrue(req.Root.T_MSG.MT3200000000.PRODUCTOS == "");
            //<?xml version="1.0"?><T_MSG MTI="3200" PCODE="000000" TID="4D790752" 
            //CID="00000000" NID="4D790752" VAPP="1.10.99PA" TRC="1" LTE="1" COP="001" DTMP="20160627T142044"><MT3200000000 PAN="6371170100894749" TOTAL="18485" TICKET="111" PAY_METH="0"><PRODUCTOS /></MT3200000000></T_MSG>

        }

      
        [TestMethod]
        public void Check3810SerializedDataIsCorrect_Test()
        {
            //<?xml version="1.0"?>
            //<S_MSG MTI="3810" PCODE="000000" CID="00000000" TID="4D790754" LTE="1" TRC="0" COP="001" DTMS="20160624T110142">
            //<MT3810000000 COD_RES="00" MSG_1="TRANSACCION" MSG_2="APROBADA" S_ID="2541" S_CODE="00000038" S_NAME="RINCON MINIMARKET" S_TAX_ID="20223287086" 
            //S_ADDRESS="AMERICA CENTRAL Y AMERICA DEL SUR" S_PHONE="" S_DISCOUNT="10"><BINES CantBINES="1"><BI Codigo="01" BIN_Min="637117" BIN_Max="637117" PAN_Len="16" /></BINES><FAMILIAS CantFAMILIAS="0" /></MT3810000000></S_MSG>

            var parser = new XmlParser();
            var rsp = new XmlResponseStructure();

            rsp.CID = "00000000";
            rsp.TID = "4D790754";
            rsp.MTI = "3810";
            rsp.PCODE = "000000";
            rsp.VAPP = "1.10.99PA";
            rsp.TRC = "0";
            rsp.LTE = "1";
            rsp.COP = "001";
            rsp.DTMS = "20160624T110142";
            rsp.Response = new MT3810000000();
            (rsp.Response as MT3810000000).COD_RES = "00";
            (rsp.Response as MT3810000000).MSG_1 = "TRANSACCION";
            (rsp.Response as MT3810000000).MSG_2 = "APROBADA";
            (rsp.Response as MT3810000000).S_ID = "2541";
            (rsp.Response as MT3810000000).S_CODE = "00000038";
            (rsp.Response as MT3810000000).S_NAME = "RINCON MINIMARKET";
            (rsp.Response as MT3810000000).S_TAX_ID = "20223287086";
            (rsp.Response as MT3810000000).S_ADDRESS = "AMERICA CENTRAL Y AMERICA DEL SUR";
            (rsp.Response as MT3810000000).S_PHONE = "";
            (rsp.Response as MT3810000000).S_DISCOUNT = "10";
            (rsp.Response as MT3810000000).BINES.CantBINES = "1";
            (rsp.Response as MT3810000000).BINES.BI.Add(new MT3810000000.BINESElement.BINESElementItem() { BIN_Max = "637117", BIN_Min = "637117", Codigo = "01", PAN_Len = "16" });
            (rsp.Response as MT3810000000).FAMILIAS.CantFAMILIAS = "0";

            parser.ResponseStructure = rsp;
            parser.Assemble();
            var output = (parser.ResponseStream as XmlStream).Get();
            var stub = "<?xml version=\"1.0\"?><S_MSG MTI=\"3810\" PCODE=\"000000\" CID=\"00000000\" TID=\"4D790754\" LTE=\"1\" TRC=\"0\" COP=\"001\" DTMS=\"20160624T110142\"><MT3810000000 COD_RES=\"00\" MSG_1=\"TRANSACCION\" MSG_2=\"APROBADA\" S_ID=\"2541\" S_CODE=\"00000038\" S_NAME=\"RINCON MINIMARKET\" S_TAX_ID=\"20223287086\" S_ADDRESS=\"AMERICA CENTRAL Y AMERICA DEL SUR\" S_PHONE=\"\" S_DISCOUNT=\"10\"><BINES CantBINES=\"1\"><BI Codigo=\"01\" BIN_Min=\"637117\" BIN_Max=\"637117\" PAN_Len=\"16\" /></BINES><FAMILIAS CantFAMILIAS=\"0\" /></MT3810000000></S_MSG>";
            Assert.IsTrue(output == String.Format("{0}", stub));
        }

        [TestMethod]
        public void Check3210SerializedDataIsCorrect_Test()
        {
            //<?xml version="1.0"?><S_MSG  ><MT3210000000 COD_RES="00" MSG_1="TRANSACCION" MSG_2="APROBADA" TRX="295268"
            //POINTS="220" TOT_POINTS="220" DISCOUNT="0" COST="0,00" DISCOUNT_AMOUNT="0,00" TOTAL_FINAL="220,00" NAME="" FOOTER=""
            //UPD_FLG="0" /></S_MSG>



            var parser = new XmlParser();
            var rsp = new XmlResponseStructure();

            rsp.CID = "00000000";
            rsp.TID = "4D790756";
            rsp.MTI = "3210";
            rsp.PCODE = "000000";
            rsp.VAPP = "1.10.99PA";
            rsp.TRC = "38";
            rsp.LTE = "1";
            rsp.COP = "001";
            rsp.DTMS = "20160623T205603";
            rsp.NID = "4D790756";
            rsp.Response = new MT3210000000();
            (rsp.Response as MT3210000000).COD_RES = "00";
            (rsp.Response as MT3210000000).MSG_1 = "TRANSACCION";
            (rsp.Response as MT3210000000).MSG_2 = "APROBADA";
            (rsp.Response as MT3210000000).TRX = "295268";
            (rsp.Response as MT3210000000).POINTS = "220";
            (rsp.Response as MT3210000000).TOT_POINTS = "220";
            (rsp.Response as MT3210000000).TOTAL_FINAL = "220,00";
            (rsp.Response as MT3210000000).POINTS = "220";
            (rsp.Response as MT3210000000).DISCOUNT = "0";
            (rsp.Response as MT3210000000).COST = "0,00";
            (rsp.Response as MT3210000000).DISCOUNT_AMOUNT = "0,00";
            (rsp.Response as MT3210000000).NAME = "";
            (rsp.Response as MT3210000000).FOOTER = "";
            (rsp.Response as MT3210000000).UPD_FLG = "0";


            parser.ResponseStructure = rsp;
            parser.Assemble();
            var output = (parser.ResponseStream as XmlStream).Get();
            var stub = "<?xml version=\"1.0\"?><S_MSG MTI=\"3210\" PCODE=\"000000\" CID=\"00000000\" TID=\"4D790756\" LTE=\"1\" TRC=\"38\" COP=\"001\" DTMS=\"20160623T205603\"><MT3210000000 COD_RES=\"00\" MSG_1=\"TRANSACCION\" MSG_2=\"APROBADA\" TRX=\"295268\" POINTS=\"220\" TOT_POINTS=\"220\" DISCOUNT=\"0\" COST=\"0,00\" DISCOUNT_AMOUNT=\"0,00\" TOTAL_FINAL=\"220,00\" NAME=\"\" FOOTER=\"\" UPD_FLG=\"0\" /></S_MSG>";
            Assert.IsTrue(output == String.Format("{0}", stub));
        }

        
        public class MT3810000000 : XmlRootResponseElement
        {
            //<MT3810000000 COD_RES="00" MSG_1="TRANSACCION" MSG_2="APROBADA" S_ID="2541" S_CODE="00000038" S_NAME="RINCON MINIMARKET"
            //S_TAX_ID="20223287086" S_ADDRESS="AMERICA CENTRAL Y AMERICA DEL SUR" S_PHONE="" S_DISCOUNT="10">
            //<BINES CantBINES="1">
            //<BI Codigo="01" BIN_Min="637117" BIN_Max="637117" PAN_Len="16" />
            //</BINES>
            //<FAMILIAS CantFAMILIAS="0" /></MT3810000000>

            [XmlAttribute]
            public string S_ID { get; set; }
            [XmlAttribute]
            public string S_CODE { get; set; }
            [XmlAttribute]
            public string S_NAME { get; set; }
            [XmlAttribute]
            public string S_TAX_ID { get; set; }
            [XmlAttribute]
            public string S_ADDRESS { get; set; }
            [XmlAttribute]
            public string S_PHONE { get; set; }
            [XmlAttribute]
            public string S_DISCOUNT { get; set; }
            public BINESElement BINES { get; set; }
            public FAMILIASElement FAMILIAS { get; set; }

            public MT3810000000()
            {
                BINES = new BINESElement();
                FAMILIAS = new FAMILIASElement();
            }
            public class BINESElement
            {
                [XmlAttribute]
                public string CantBINES { get; set; }
                [XmlElement("BI")]
                public List<BINESElementItem> BI { get; set; }


                public BINESElement()
                {
                    BI = new List<BINESElementItem>();
                }

                public class BINESElementItem
                {
                    [XmlAttribute]
                    public string Codigo { get; set; }//Codigo="01"
                    [XmlAttribute]
                    public string BIN_Min { get; set; } //BIN_Min="637117"
                    [XmlAttribute]
                    public string BIN_Max { get; set; } //BIN_Max="637117"
                    [XmlAttribute]
                    public string PAN_Len { get; set; } //PAN_Len="16"
                }
            }

            public class FAMILIASElement
            {
                [XmlAttribute]
                public string CantFAMILIAS { get; set; }
            }

        }
        public class MT3210000000 : XmlRootResponseElement
        {
            //<MT3210000000 COD_RES="00" MSG_1="TRANSACCION" MSG_2="APROBADA" TRX="295268"
            //POINTS="220" TOT_POINTS="220" DISCOUNT="0" COST="0,00" DISCOUNT_AMOUNT="0,00" TOTAL_FINAL="220,00" NAME="" FOOTER=""
            //UPD_FLG="0" />           
            [XmlAttribute]
            public string TRX { get; set; }
            [XmlAttribute]
            public string POINTS { get; set; }
            [XmlAttribute]
            public string TOT_POINTS { get; set; }
            [XmlAttribute]
            public string DISCOUNT { get; set; }
            [XmlAttribute]
            public string COST { get; set; }
            [XmlAttribute]
            public string DISCOUNT_AMOUNT { get; set; }
            [XmlAttribute]
            public string TOTAL_FINAL { get; set; }
            [XmlAttribute]
            public string NAME { get; set; }
            [XmlAttribute]
            public string FOOTER { get; set; }
            [XmlAttribute]
            public string UPD_FLG { get; set; }

        }
    }
}
