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
        public string OperationId { get; set; }
        public string MerchantId { get; set; }
        public string TerminalId { get; set; }
        public List<Item> Items { get; set; }

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
            Items = new List<Item>();
        
        }

        public override object GetOperationId()
        {
            return OperationId;
        }

        public class Item
        {
            public string Product { get; set; }
            public string ClienteReference { get; set; }
            public decimal? Amount { get; set; }
        }
    }
}
