using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool GetRequirementDelegate();
    public delegate bool BuildResponseDelegate();
    public delegate bool ReplyDelegate();
    

    public interface ITransactionHandlerListenable
    {
        bool GetRequirement();
        bool BuildResponse();
        bool Reply();        
    }    
}
