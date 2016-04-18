using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Beltran
{
    public class BeltranSubfield : AbstractTransactionParserSubfield
    {
        public string Content = null;

        public override int CopyContentFrom(byte[] src)
        {
            if (src.Length < (this.Offset + this.Length)) this.Length = src.Length - this.Offset;

            Content = AbstractTransactionFacade.GetString(src, this.Length, this.Offset);
            base.Content = new byte[Content.Length + 1];
            return base.CopyContentFrom(src);
        }

        public override void CreateContent() { }
    }
}
