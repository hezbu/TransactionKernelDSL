using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.NoProtocol
{
    public class NoProtocolParserStructure : AbstractTransactionParserStructure
    {

        public NoProtocolParserStructure(
                                    string rootSection = "NoProtocol"
                                ) :
            base(rootSection,AbstractTransactionParserStructureType.Request)
        {

        }
        public override object GetOperationId()
        {
            return "X";
        }
    }
}
