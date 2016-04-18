using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionParserSubfield
    {
        public int Id { get; set; }
        public string Keyname { get; set; }
        public Nullable<AbstractTransactionParserFieldType> Type { get; set; }
        public string Description { get; set; }
        public int Length { get; set; }
        public int Offset { get; set; }
        public byte[] Content { get; set; }

        #region Virtual Methods
        public virtual void CreateContent()
        {
            switch (Type)
            {
                case AbstractTransactionParserFieldType.BCD: //"BCD"
                    this.Content = new byte[(this.Length * 2) + 1];
                    break;
                default:
                    this.Content = new byte[this.Length + 1];
                    break;
            }
        }
        public virtual int CopyContentFrom(byte[] src)
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");

            switch (Type)
            {
                case AbstractTransactionParserFieldType.BCD: //"BCD":
                    Buffer.BlockCopy(src, Offset * 2, this.Content, 0, this.Length * 2);
                    break;
                case AbstractTransactionParserFieldType.BIN: //"BIN":
                case AbstractTransactionParserFieldType.ASCII: //"ASCII":
                    Buffer.BlockCopy(src, Offset, this.Content, 0, this.Length);
                    break;
                default: throw new ApplicationException("CopyContentFrom for type " + Type + " is not known");

            }

            return this.Length;
        }
        public virtual int TrueLength
        {
            get
            {
                switch (Type)
                {
                    case AbstractTransactionParserFieldType.ASCII:
                    case AbstractTransactionParserFieldType.BCD:
                    case AbstractTransactionParserFieldType.BIN:
                        return Length;
                    default:
                        throw new ApplicationException("Type " + Type + " for TrueLength is not known");
                }
            }
        }
        #endregion

        public override string ToString()
        {
            if (Content == null) throw new ApplicationException("Content is not initialized. Please use CreateContent() method first");
            return AbstractTransactionFacade.GetString(this.Content, this.Content.Length - 1);
        }
    }
}
