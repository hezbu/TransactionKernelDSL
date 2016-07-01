using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Json
{
    public class JsonParserStructure: AbstractTransactionParserStructure
    {     
        public string MTI { get; set; }
        public string P_CODE { get; set; }
        public string CID { get; set; }
        public string TID { get; set; }
        public string TRX { get; set; }
        public string COD_OPER { get; set; }
        public JsonStructureRequestInfo RequestInfo { get; set; }
        public JsonStructureResponseInfo ResponseInfo { get; set; }
      

        [JsonIgnore]
        public new string Logger 
        {
            set
            {
                base.Logger = value;
            }
        }

        [JsonIgnore]
        public override Nullable<AbstractTransactionParserStructureType> StructureType
        {
            get
            {
                return base.StructureType;
            }
        }

        [JsonIgnore]
        public override Dictionary<int, AbstractTransactionParserField> Fields
        {
            get
            {
                return base.Fields;
            }
        }

        public JsonParserStructure(Nullable<AbstractTransactionParserStructureType> type,
                                string rootSection = "Json"
                                ) :
            base(rootSection, type)
        {
           
        }

        public override object GetOperationId()
        {
            return String.Format("{0}{1}", MTI,P_CODE);
        }

      
    }

    public class JsonStructureResponseInfo
    {
        public string COD_RES { get; set; }
        public string MSG_RES { get; set; }
    }

    public class JsonStructureRequestInfo
    {
        public string PAN { get; set; }
    }
}
