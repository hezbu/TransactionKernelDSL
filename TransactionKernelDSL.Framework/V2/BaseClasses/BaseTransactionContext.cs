using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V2.Enums;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.V2.BaseClasses
{
    public abstract class BaseTransactionContext : ITransactionContext
    {
        protected Dictionary<string, string> _ContextValues;
        protected TransactionStatus _TransactionStatus = TransactionStatus.TransactionNotProcessed;
        protected TransmissionStatus _TransmissionStatus = TransmissionStatus.NoError;

        protected DateTime _TransactionDatetime;
        protected string _ThreadId = null;

        protected string _OperationId = null;
        protected string _ErrorCode = null;
        protected string _ErrorMessage = null;

        protected BaseTransactionContext()
        {
            _ContextValues = new Dictionary<string, string>();
            _ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            _TransactionDatetime = DateTime.Now;
        }


        public virtual DateTime TransactionDatetime
        {
            get
            {
                return _TransactionDatetime;
            }
        }
        public virtual string ErrorCode
        {
            get
            {
                return (String.IsNullOrEmpty(this._ErrorCode) ? String.Empty : this._ErrorCode);
            }
            set
            {
                this._ErrorCode = value;
            }
        }
        public virtual string ErrorMessage
        {
            get
            {
                return (String.IsNullOrEmpty(this._ErrorMessage) ? String.Empty : this._ErrorMessage);
            }
            set
            {
                this._ErrorMessage = value;
            }
        }
        public virtual TransactionStatus TransactionStatus
        {
            get
            {
                return _TransactionStatus;
            }
            set
            {
                _TransactionStatus = value;
            }
        }
        public virtual TransmissionStatus TransmissionStatus
        {
            get
            {
                return _TransmissionStatus;
            }
            set
            {
                _TransmissionStatus = value;
            }
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this._ContextValues.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._ContextValues.GetEnumerator();
        }

        public string this[string keyNameParam]
        {
            get
            {
                if (this._ContextValues.ContainsKey(keyNameParam) == true) return this._ContextValues[keyNameParam];
                else throw new ApplicationException("Key/Value pair doesn't exist: K=" + keyNameParam);
            }
            set
            {
                this._ContextValues[keyNameParam] = value;
            }
        }

               
    }
}
