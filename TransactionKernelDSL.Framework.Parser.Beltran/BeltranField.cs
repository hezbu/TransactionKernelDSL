using TransactionKernelDSL.Framework.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.Parser.Beltran
{
    public class BeltranField : AbstractTransactionParserField
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

        public int CopyContentFrom(byte[] src, int offset = 0, int? trueLength = null)
        {
            if (trueLength.HasValue == true)
            {
                if (this.Length < trueLength.Value) throw new ApplicationException(String.Format("Length of field {0} is bigger than configured in app.config ({1}/{2})", this.Keyname, this.Length, trueLength.Value));
                this.Length = trueLength.Value;
                this.CreateContent();
            }

            this._Content = AbstractTransactionFacade.GetString(src, Math.Min(this.Length, src.Length), offset);
            return base.CopyContentFrom(src, offset);
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
