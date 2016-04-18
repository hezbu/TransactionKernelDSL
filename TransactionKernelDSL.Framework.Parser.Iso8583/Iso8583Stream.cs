using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583
{
    public sealed class Iso8583Stream : AbstractTransactionParserStream
    {
        public const int Iso8583MaxLength = 8192;

        public Iso8583Stream() : 
            base(Iso8583MaxLength)
        {

        }

        public override string ToString()
        {
            string strDump = "";

            for (int i = 0; i < this._Stream.Length; i++)
            {
                strDump += " " + this._Stream[i].ToString("X").PadLeft(2, '0');
            }

            return strDump;
        }
    }
}
