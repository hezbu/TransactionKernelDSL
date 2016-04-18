using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionParserStream
    {
        protected byte[] _Stream;
        protected bool _IsSet = false;

        public virtual bool IsSet
        {
            get
            {
                return _IsSet;
            }
        }

        #region Constructor
        public AbstractTransactionParserStream(int maxStreamLength)
        {
            _Stream = new byte[maxStreamLength];
        }
        #endregion

        public byte this[int index]
        {
            get
            {
                return _Stream[index];
            }
            set
            {
                _Stream[index] = value;
            }
        }

        #region Member Methods
        public virtual void Set(byte[] stream, int? length = null)
        {
            int len = 0;
            if (length.HasValue == false) len = stream.Length;
            else len = length.Value;

            _Stream = new byte[len];           
            Buffer.BlockCopy(stream, 0, _Stream, 0, len);
            _IsSet = true;
        }
        public virtual byte[] Get()
        {
            return _Stream;
        }

        public new abstract string ToString();
        #endregion
    }
}
