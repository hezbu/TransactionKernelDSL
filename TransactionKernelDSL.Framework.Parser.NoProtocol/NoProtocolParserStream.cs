using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.NoProtocol
{
    public class NoProtocolParserStream : AbstractTransactionParserStream
    {
        public const int NoProtocolStreamMaxLength = 8192;

        public NoProtocolParserStream() :
            base(NoProtocolStreamMaxLength)
        {

        }

        public override string ToString()
        {
            return DumpHelper.Dump(this._Stream, this._Stream.Length);
        }
    }
}
