using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.Parser.Iso8583
{
    public sealed class Iso8583Field : AbstractTransactionParserField
    {        
        public int CopyContentFrom(string src)
        {
            switch (this.Type)
            {
                default:
                case AbstractTransactionParserFieldType.ASCII:
                    return base.CopyContentFrom(AbstractTransactionFacade.GetBytes(src));
                case AbstractTransactionParserFieldType.BCD:
                    return base.CopyContentFrom(AbstractTransactionFacade.GetHexaBytes(src));
                case AbstractTransactionParserFieldType.LLVAR:
                    return base.CopyContentFrom(AbstractTransactionFacade.GetLLVarBytes(src));
                case AbstractTransactionParserFieldType.ASCII_LVAR:
                    return base.CopyContentFrom(AbstractTransactionFacade.GetAsciiLVarBytes(src));
            }
        }



        public string ToString(bool includeLen)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (this.Type)
            {
                //case AbstractTransactionParserFieldType.LVAR:
                //    {
                //        string result = "(" + this.Content[0].ToString("X2") + ")";
                //        result += AbstractTransactionFacade.GetString(this.Content, this.Length - 1, 1);
                //        return result;
                //    }

                case AbstractTransactionParserFieldType.LLVAR:
                    {
                        string result = "";
                        if (includeLen == true)
                        {
                            result = "(" + this.Content[0].ToString("X2") + this.Content[1].ToString("X2") + ")";
                        }
                        result += AbstractTransactionFacade.GetString(this.Content, this.Length - 2, 2);
                        return result;
                    }
                default:
                    return AbstractTransactionFacade.GetString(this.Content, this.Content.Length - 1);
            }
        }

        public string ToBinString(bool includeLen)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (this.Type)
            {
                //case AbstractTransactionParserFieldType.LVAR:
                //    {
                //        string result = "(" + this.Content[0].ToString("X2") + ")";
                //        result += AbstractTransactionFacade.GetString(this.Content, this.Length - 1, 1);
                //        return result;
                //    }

                case AbstractTransactionParserFieldType.LLVAR:
                    {
                        string result = "";
                        if (includeLen == true)
                        {
                            result = "(" + this.Content[0].ToString("X2") + this.Content[1].ToString("X2") + ")";
                        }
                        result += AbstractTransactionFacade.GetBinString(this.Content, this.Length - 2, 2);
                        return result;
                    }
                default:
                    return AbstractTransactionFacade.GetBinString(this.Content, this.Content.Length - 1);
            }
        }

        public override string ToString()
        {
            return this.ToString(true);
        }
    }
}
