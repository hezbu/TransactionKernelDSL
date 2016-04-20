using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.V2.BaseClasses
{
    public abstract class BaseTransactionEngine : ITransactionEngine
    {
        protected BaseTransactionEngine()
        {
            _IsEngineOn = false;
        }

        private bool _IsEngineOn;
     
        public virtual bool IsEngineOn { get { return _IsEngineOn; } }

        public virtual bool Start()
        {
            _IsEngineOn = true;
            return true;
        }

        public virtual bool Stop()
        {
            _IsEngineOn = false;
            return true;
        }

        public static class Factory
        {
             private static BaseTransactionEngine _Instance = null;

             public static BaseTransactionEngine Build<E>() where E : BaseTransactionEngine
             {
                 if (_Instance == null)
                 {
                     _Instance = (E)Activator.CreateInstance(typeof(E), true); 
                 }

                 return _Instance;
             }
        }

     
    }
}
