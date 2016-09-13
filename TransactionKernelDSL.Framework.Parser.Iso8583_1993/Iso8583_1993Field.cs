using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993
{
    public sealed class Iso8583_1993Field : AbstractTransactionParserField
    {
        public override string ToString()
        {
            return AbstractTransactionFacade.GetString(base.Content, base.Length);
        }

        public int CopyContentFrom(string src)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");
            return this.CopyContentFrom(AbstractTransactionFacade.GetBytes(src));
        }

        public override int CopyContentFrom(byte[] src, int offset = 0)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (Type)
            {
                case AbstractTransactionParserFieldType.LLVAR:
                    {
                        byte[] length = new byte[2 + 1];
                        System.Buffer.BlockCopy(src, offset, length, 0, 2);
                        int intFieldLength = 0;
                        if (Int32.TryParse(AbstractTransactionFacade.GetString(length, 2), out intFieldLength) == false) throw new ApplicationException("LLVAR length at AttendedTerminal " + this.Keyname + " is not numeric ->" + AbstractTransactionFacade.GetString(length, 2));

                        if (intFieldLength < this.Content.Length)
                        {
                            this.Length = intFieldLength + 2;
                            this.Content = new byte[this.Length + 1];
                            System.Buffer.BlockCopy(src, offset, this.Content, 0, intFieldLength + 2);
                            return this.Length;
                        }
                        else throw new Exception("Invalid LLVAR Length for " + this.Keyname + ": " + intFieldLength.ToString());

                    }
                case AbstractTransactionParserFieldType.LLLVAR:
                    {
                        byte[] length = new byte[3 + 1];
                        System.Buffer.BlockCopy(src, offset, length, 0, 3);
                        int intFieldLength = 0;
                        if (Int32.TryParse(AbstractTransactionFacade.GetString(length, 3), out intFieldLength) == false) throw new ApplicationException("LLLVAR length at AttendedTerminal " + this.Keyname + " is not numeric ->" + AbstractTransactionFacade.GetString(length, 3));

                        if (intFieldLength < this.Content.Length)
                        {
                            this.Length = intFieldLength + 3;
                            this.Content = new byte[this.Length + 1];
                            System.Buffer.BlockCopy(src, offset, this.Content, 0, intFieldLength + 3);
                            return this.Length;
                        }
                        else throw new Exception("Invalid LLLVAR Length for " + this.Keyname + ": " + intFieldLength.ToString());

                    }
                default: return base.CopyContentFrom(src, offset);
            }

        }

        public override int CopyContentTo(ref byte[] dest, int offset = 0)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");
            switch (Type)
            {
                case AbstractTransactionParserFieldType.LLLVAR:
                    Buffer.BlockCopy(this.Content, 0, dest, offset, this.Length);
                    return this.Length;
                default: return base.CopyContentTo(ref dest, offset);
            }
        }
    }
}
