using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public class AbstractTransactionContext
    {
        #region Member Fields
        protected Dictionary<string, string> _ContextValues;
        protected TransactionStatus _TransactionStatus = TransactionStatus.TransactionNotProcessed;
        protected TransmissionStatus _TransmissionStatus = TransmissionStatus.NoError;

        protected DateTime _TransactionDatetime;
        protected string _ThreadId = null;

        protected string _OperationId = null;
        protected string _ErrorCode = null;
        protected string _ErrorMessage = null; 
        #endregion

        #region Member Properties

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
        }
        public virtual string ErrorMessage
        {
            get
            {
                return (String.IsNullOrEmpty(this._ErrorMessage) ? String.Empty : this._ErrorMessage);
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
        #endregion


        #region Constructor
        public AbstractTransactionContext()
        {
            _ContextValues = new Dictionary<string, string>();
            _ThreadId = Thread.CurrentThread.ManagedThreadId.ToString();
            _TransactionDatetime = DateTime.Now;
        }
        #endregion

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

        public virtual void SetError(string errorCodeParam, string errorMessageParam = null)
        {
            this._ErrorCode = errorCodeParam;
            this._ErrorMessage = errorMessageParam;
        }

        
    }
}
