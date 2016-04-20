using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2.BaseClasses
{
    public abstract class BaseTransactionFacade
    {
        public abstract bool StartEngines();
        public abstract bool StopEngines();
    }
}
