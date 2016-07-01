using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Xml
{
    public class XmlRequestStructure : AbstractTransactionParserStructure
    {
        public dynamic Root { get; set; }

        public XmlRequestStructure() :
            base(String.Empty, AbstractTransactionParserStructureType.Request)
        {
            Root = new ExpandoObject();
        }

        public override object GetOperationId()
        {
            return String.Format("{0}{1}", Root.T_MSG.MTI, Root.T_MSG.PCODE);
        }

    }

    //[XmlRoot("T_MSG")]
    //public class XmlRootRequestElement
    //{

    //    [XmlAttribute]
    //    public string MTI { get; set; }
    //    [XmlAttribute]
    //    public string PCODE { get; set; }
    //    [XmlAttribute]
    //    public string CID { get; set; }
    //    [XmlAttribute]
    //    public string TID { get; set; }
    //    [XmlAttribute]
    //    public string NID { get; set; }
    //    [XmlAttribute]
    //    public string VAPP { get; set; }
    //    [XmlAttribute]
    //    public string TRC { get; set; }
    //    [XmlAttribute]
    //    public string LTE { get; set; }
    //    [XmlAttribute]
    //    public string COP { get; set; }
    //    [XmlAttribute]
    //    public string DTMP { get; set; }       
    //}
}
