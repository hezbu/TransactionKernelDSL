using TransactionKernelDSL.Framework.V1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Beltran
{
    public class BeltranStream : AbstractTransactionParserStream
    {
        public const byte PIPE = 0x7C;
        public const int BeltranMaxLength = 8192;

        public BeltranStream() :
            base(BeltranMaxLength)
        {

        }

        private int _Length;

        public int Length
        {
            get { return _Length; }
        }
        
        public string Content { get; set; }

        public override byte[] Get()
        {
            return base.Get();
        }

        public override void Set(byte[] stream, int? length = null)
        {
            this.Content = AbstractTransactionFacade.GetAsciiString(stream, length.HasValue ? length.Value : stream.Length);
            base.Set(stream, length);
        }

        public override string ToString()
        {
            return Content;
        }

    }
}
