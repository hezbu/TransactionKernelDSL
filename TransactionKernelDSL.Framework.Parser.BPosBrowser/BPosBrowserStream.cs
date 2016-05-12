using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser
{
    public class BPosBrowserStream : AbstractTransactionParserStream
    {
        public const int BPosBrowserMaxLength = 8192;
        private string _Content;


        public BPosBrowserStream() :
            base(BPosBrowserMaxLength)
        {

        }

        public new string Get()
        {
            return _Content;
        }

        public string Get(int startIndex)
        {
            return _Content.Substring(startIndex);
        }

        public string Get(int startIndex, int length)
        {
            return _Content.Substring(startIndex, length);
        }

        public override void Set(byte[] stream, int? length = null)
        {
            _Content = AbstractTransactionFacade.GetString(stream, (length == null ? stream.Length : length.Value));
            base.Set(stream, length);
        }

        public void Set(string value)
        {
            this._Content = value;
            base.Set(AbstractTransactionFacade.GetBytes(this._Content), this._Content.Length);
        }

        public override string ToString()
        {
            return _Content;
        }
    }
}
