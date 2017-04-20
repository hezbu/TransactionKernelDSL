using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Http
{
    public class HttpStructure : AbstractTransactionParserStructure
    {
        public HttpStructure()
            : base("HTTP",null)
        {

        }
        public override object GetOperationId() 
        {
            return "X";
        }
    }
}
