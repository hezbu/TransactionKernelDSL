using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2.Interfaces
{
    public interface ITransactionEngine
    {
        bool IsEngineOn { get;}
        bool Start();
        bool Stop();
    }
}
