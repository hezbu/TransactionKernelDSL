using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool PreProcessTransactionDelegate();
    public delegate bool PostProcessTransactionDelegate();
    public delegate bool FinalPostProcessTransactionDelegate();

    public interface ITransactionHandlerPersistable
    {
        bool PreProcessTransaction();     
        bool PostProcessTransaction();
        bool FinalPostProcessTransaction();
    }
}
