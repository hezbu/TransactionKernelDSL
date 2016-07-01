using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Xml
{
    public class XmlResponseStructure : AbstractTransactionParserStructure
    {

        public string MTI { get; set; }
        public string PCODE { get; set; }
        public string CID { get; set; }
        public string TID { get; set; }
        public string NID { get; set; }
        public string VAPP { get; set; }
        public string TRC { get; set; }
        public string LTE { get; set; }
        public string COP { get; set; }
        public string DTMP { get; set; }
        public XmlRootResponseElement Response { get; set; }        
      

        public XmlResponseStructure() :
            base(String.Empty,AbstractTransactionParserStructureType.Response)
        {

        }

        public override object GetOperationId()
        {
            return String.Format("{0}{1}", MTI, PCODE);
        }


        public string DTMS { get; set; }
    }

    public abstract class XmlRootResponseElement
    {

        [XmlAttribute]
        public string COD_RES { get; set; }
        [XmlAttribute]
        public string MSG_1 { get; set; }
        [XmlAttribute]
        public string MSG_2 { get; set; }

        

        public virtual string Serialize()
        {
            using (StringWriter sw = new StringWriter())
            {
                var settings = new XmlWriterSettings();            
                settings.OmitXmlDeclaration = true;

                using (XmlWriter tw = XmlWriter.Create(sw, settings))
                {
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(this.GetType());
                        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                        
                        ns.Add("", "");
                        serializer.Serialize(tw, this, ns);
                    }
                    catch (Exception ex)
                    {
                        //Handle Exception Code
                    }                   
                }

                return sw.ToString();
            }
        }

    }

   

}
