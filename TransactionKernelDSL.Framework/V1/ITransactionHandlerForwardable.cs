using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate bool BuildRequirementDelegate();
    public delegate bool ResolveDelegate();
    public delegate bool GetResponseDelegate();
    public delegate bool ForwardHandlerFactoryDelegate();
    public delegate bool ProcessTransactionDelegate();
 
    public interface ITransactionHandlerForwardable
    {
        bool BuildRequirement();      
        bool Resolve();      
        bool GetResponse();
        bool ForwardHandlerFactory();
        bool ProcessTransaction();
    }
}
