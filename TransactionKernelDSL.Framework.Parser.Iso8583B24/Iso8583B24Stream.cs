using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583B24
{
    public sealed class Iso8583B24Stream : AbstractTransactionParserStream
    {
        public const int Iso8583B24MaxLength = 8192;

        public Iso8583B24Stream() :
            base(Iso8583B24MaxLength)
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

        public void Set(string stream)
        {
            byte[] aux = new byte[stream.Length + 2];
            Buffer.BlockCopy(AbstractTransactionFacade.GetBcdBytes(stream.Length.ToString("X4")), 0, aux, 0, 2);
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(stream), 0, aux, 2, stream.Length);
            base.Set(aux);
           
        }
    }
}
