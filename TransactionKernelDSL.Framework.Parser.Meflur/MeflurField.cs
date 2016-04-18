using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Meflur
{
    public class MeflurField : AbstractTransactionParserField
    {
        private string _Content = null;

        public string Content
        {
            get { return _Content; }
            set
            {
                _Content = value.Substring(0, Math.Min(value.Length, this.Length));
                base.Content = AbstractTransactionFacade.GetBytes(_Content);
            }
        }
        public bool HasContent
        {
            get
            {
                // return !String.IsNullOrEmpty(_Content); 
                return _Content != null;
            }
        }

        #region Overridden Methods
        public override void CreateContent() { }
        public override int CopyContentFrom(byte[] src, int offset = 0)
        {
            _Content = AbstractTransactionFacade.GetString(src, Math.Min(this.Length, src.Length), offset);
            base.Content = new byte[_Content.Length + 1];
            return base.CopyContentFrom(AbstractTransactionFacade.GetBytes(_Content)); ;
        }
        public override int CopyContentTo(ref byte[] dest, int offset = 0)
        {
            Buffer.BlockCopy(AbstractTransactionFacade.GetBytes(this._Content), 0, dest, offset, Math.Min(this.Length, this._Content.Length));
            return Math.Min(this.Length, this._Content.Length);
        }
        public override int TrueLength
        {
            get
            {
                return _Content.Length;
            }
        }
        public override string ToString()
        {
            return Content;
        }
        #endregion
    }
}
