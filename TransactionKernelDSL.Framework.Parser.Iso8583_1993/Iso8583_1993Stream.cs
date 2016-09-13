using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993
{
    public sealed class Iso8583_1993Stream : AbstractTransactionParserStream
    {
        public const int Iso8583_1993MaxLength = 8192;

        public Iso8583_1993Stream() :
            base(Iso8583_1993MaxLength)
        {

        }

        public string ToString(bool includeLen)
        {
            if (includeLen == true) return "(" + AbstractTransactionFacade.GetString(_Stream, _Stream.Length).Substring(2).Length.ToString("X4") + ")" + AbstractTransactionFacade.GetString(_Stream, _Stream.Length).Substring(2);
            else return AbstractTransactionFacade.GetString(_Stream, _Stream.Length).Substring(2);
        }

        public override string ToString()
        {
            return ToString(true);
        }               
    }
}
