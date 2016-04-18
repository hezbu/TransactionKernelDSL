using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionParser       
    {
        #region Member Fields
        protected ILog _Log = null;
        protected string _ErrorMessage = null;
        protected TransmissionStatus _Status = TransmissionStatus.NoError;
        #region ITransactionParserAssembleable Member Fields
        protected AssembleDelegate _AssembleMethod = null;
        protected DisassembleDelegate _DisassembleMethod = null;
        #endregion
        #region ITransactionParserCommunicable Member Fields
        protected SendDelegate _SendMethod = null;
        protected ReceiveDelegate _ReceiveMethod = null;
        protected IsKeepAliveMessageDelegate _IsKeepAliveMessageMethod = null;
        #endregion

        protected AbstractTransactionParserStream _RequestStream = null;
        protected AbstractTransactionParserStream _ResponseStream = null;
        protected AbstractTransactionParserStructure _RequestStructure = null;
        protected AbstractTransactionParserStructure _ResponseStructure = null;
        protected bool _IsKeepAliveMessage = false;
        protected string _RootSection = null;
        protected bool _IsInputParser = true;

        #endregion
        #region Member Properties
        public AssembleDelegate AssembleMethod { get { return _AssembleMethod; } }
        public DisassembleDelegate DisassembleMethod { get { return _DisassembleMethod; } }
        public SendDelegate SendMethod { get { return _SendMethod; } }
        public ReceiveDelegate ReceiveMethod { get { return _ReceiveMethod; } }
        public IsKeepAliveMessageDelegate IsKeepAliveMessageMethod { get { return _IsKeepAliveMessageMethod; } }
        
        public virtual AbstractTransactionParserStructure RequestStructure
        {
            get
            {
                return _RequestStructure;
            }
            set 
            {
                _RequestStructure = value;
            }
        }
        public virtual AbstractTransactionParserStructure ResponseStructure
        {
            get
            {
                return _ResponseStructure;
            }
            set
            {
                _ResponseStructure = value;
            }
        }

        public virtual AbstractTransactionParserStream RequestStream
        {
            get
            {
                return _RequestStream;
            }
            set
            {
                _RequestStream = value;
            }
        }
        public virtual AbstractTransactionParserStream ResponseStream
        {
            get
            {
                return _ResponseStream;
            }
            set
            {
                _ResponseStream = value;
            }
        }
               
        public string ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
        }
        public TransmissionStatus Status
        {
            get
            {
                return _Status;
            }
        }
        public string Logger
        {
            set
            {
                _Log = LogManager.GetLogger(value);
            }
        }
        #endregion

        #region Constructor
        public AbstractTransactionParser(string rootSection, bool isListenerParser = true)
        {
            _Log = LogManager.GetLogger("MainLogger");
            _RootSection = rootSection;
            _IsInputParser = isListenerParser;
        } 
        #endregion

        public virtual void ResetParserStatus()
        {
             _ErrorMessage = null;
             _Status = TransmissionStatus.NoError;
        }

    }
}
