using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser
{
    public class BPosBrowserStructure : AbstractTransactionParserStructure
    {
        public Dictionary<string, string> Main { get; set; }
        public Dictionary<string, string> InputValue { get; set; }

        public BPosBrowserStructure(Nullable<AbstractTransactionParserStructureType> type,
                                string rootSection = "BPosBrowserParser"
                                ) :
            base(rootSection, type)
        {

            Main = new Dictionary<string, string>();
            InputValue = new Dictionary<string, string>();
        }

        public override object GetOperationId()
        {
            if (Main.ContainsKey("MsgType"))
            {
                return Main["MsgType"];
            }
            else throw new ApplicationException("MsgType is not loaded. No valid transaction identifier found!");
        }

        public string Header { get; set; }

        public string Length { get; set; }
    }
}
