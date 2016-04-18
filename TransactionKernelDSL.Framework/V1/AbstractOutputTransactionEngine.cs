using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractOutputTransactionEngine : AbstractTransactionEngine
    {
        #region Member Fields
        //protected AbstractTransactionNexus _AssignedNexus;
        protected AbstractTransactionParser _AssignedParser;
        protected object _AssignedClient;
        protected ILog _EngineLog = null;
        #endregion

        #region Member Properties
        protected AbstractTransactionParser Parser
        {
            get { return _AssignedParser; }
            set { _AssignedParser = value; }
        }

        protected object Client
        {
            get { return _AssignedClient; }
            set { _AssignedClient = value; }
        }
        public override EngineType Type
        {
            get { return EngineType.OutputEngine; }
        }

        public virtual string EngineLogger
        {
            set
            {
                _EngineLog = LogManager.GetLogger(value);

            }
        } 
        #endregion

        #region Constructor
        protected AbstractOutputTransactionEngine() :
            base()
        {
            _EngineLog = LogManager.GetLogger("MainLogger");
        }
        #endregion

        #region Abstract and Virtual Member Methods
        public abstract TransmissionStatus Resolve(AbstractTransactionParser parser);
        #endregion

    }
}
