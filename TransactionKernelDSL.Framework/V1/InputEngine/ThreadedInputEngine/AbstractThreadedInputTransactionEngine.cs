using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate void ThreadParamlessWorkerDelegate();
    public delegate void ThreadPoolWorkerDelegate(object slaveParam);

    public abstract class AbstractThreadedInputTransactionEngine : AbstractInputTransactionEngine
    {
        protected bool _IsListening = false;

        private Thread _MainThread = null;
        protected ThreadParamlessWorkerDelegate _ThreadParamlessWorkerDelegateMethod = null;
        protected ThreadPoolWorkerDelegate _ThreadPoolWorkerDelegateMethod = null;

        

        protected AbstractThreadedInputTransactionEngine()
            : base()
        {
            
        }

        public override bool IsEngineOn
        {
            get { return _IsListening; }
        }       

        public override bool Start()
        {
            if (_ThreadParamlessWorkerDelegateMethod == null) throw new ApplicationException("Master delegate method is not set. Must be set in order to start the engine");

            _MainThread = new Thread(() => _ThreadParamlessWorkerDelegateMethod());
            _MainThread.Start();

            return _MainThread.IsAlive;
        }

        public override bool Stop()
        {
            _MainThread.Join();
            return true;
        }

        public virtual void Link(ThreadParamlessWorkerDelegate masterMethod, ThreadPoolWorkerDelegate slaveMethod)
        {
            _ThreadParamlessWorkerDelegateMethod = masterMethod;
            _ThreadPoolWorkerDelegateMethod = slaveMethod;
        }

        /// <returns>AbstractTransactionNexus</returns>
       // protected abstract AbstractTransactionNexus NexusFactory();
       
    }
}
