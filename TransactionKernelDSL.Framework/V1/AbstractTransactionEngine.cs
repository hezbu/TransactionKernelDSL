using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using log4net;
using System.Net;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionEngine
    {       
        #region Member fields
        protected ILog _Log = null;
        private DebugTransactionStatus _DebugStatus = DebugTransactionStatus.NothingHappenned;
        #endregion


        #region Member properties
        public virtual DebugTransactionStatus DebugStatus
        {
            get
            {
                return _DebugStatus;
            }
            set
            {
                _DebugStatus |= value;
            }
        }
        public virtual string Logger
        {
            set
            {
                _Log = LogManager.GetLogger(value);
            }
        }

        public abstract bool IsEngineOn
        {
            get;
        }
        public abstract EngineType Type
        {
            get;
        }
        #endregion

       
               
        #region Constructor
        protected AbstractTransactionEngine()
        {
            _Log = LogManager.GetLogger("MainLogger");            
            _DebugStatus = DebugTransactionStatus.NothingHappenned;
        }
        #endregion

        #region Member Abstract And Virtual Methods
        public abstract bool Start();
        public abstract bool Stop();
        #endregion    

        [Flags]
        public enum EngineType
        {
            InputEngine = 0x00000001,
            OutputEngine = 0x00000002,
            WebServiceEngine = 0x00000004
        }
    }
}
