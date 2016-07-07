using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Json
{
    public class JsonParserRequestStructure : AbstractTransactionParserStructure
    {
        public dynamic Root { get; set; }

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

        public JsonParserRequestStructure(
                                    string rootSection = "Json"
                                ) :
            base(rootSection,AbstractTransactionParserStructureType.Request)
        {

        }


        public override object GetOperationId()
        {
            return String.Format("{0}{1}", Root.MTI, Root.P_CODE);
        }
    }
}
