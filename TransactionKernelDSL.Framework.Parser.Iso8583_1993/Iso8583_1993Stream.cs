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

        public override string ToString()
        {
            string strDump = "";
            int length = 0;
            try
            {
                for (int i = 0; i < 18; i++)
                {
                    strDump += " " + this._Stream[i].ToString("X").PadLeft(2, '0');
                }
                length = this._Stream[16] * 256;
                length += this._Stream[17];
                for (int i = 18, j = 0; j < length; i++, j++)
                {
                    strDump += " " + this._Stream[i].ToString("X").PadLeft(2, '0');
                }
            }
            catch (Exception ex)
            {

            }

            return strDump;
        }

      
    }
}
