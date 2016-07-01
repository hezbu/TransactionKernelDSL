
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;

namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// User-customized helpers for Forwarding stage methods 
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:15
    /// </remarks>
    public partial class TL1_RedIn_BalanceQuery_Handler: AbstractTransactionHandler,ITransactionHandlerForwardable
	{    
                #region ITransactionHandlerForwardable User-Customized Members

                public bool BuildRequirement_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return BuildRequirement();

                    throw new NotImplementedException("Implementation left for BuildRequirement_UC() in TL1_RedIn_BalanceQuery_Handler");
                }                       

                public bool Resolve_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return Resolve();

                   throw new NotImplementedException("Implementation left for Resolve_UC() in TL1_RedIn_BalanceQuery_Handler");
                }
                      
                public bool GetResponse_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return GetResponse();

                    throw new NotImplementedException("Implementation left for GetResponse_UC() in TL1_RedIn_BalanceQuery_Handler");
                }

                public bool ForwardHandlerFactory_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return ForwardHandlerFactory();

                    throw new NotImplementedException("Implementation left for ForwardHandlerFactory_UC() in TL1_RedIn_BalanceQuery_Handler");
                }

                public bool ProcessTransaction_UC()
                {
                     // Uncomment this line if you want to use default behaviour.
                    // return ProcessTransaction();

                        throw new NotImplementedException("Implementation left for ProcessTransaction_UC() in TL1_RedIn_BalanceQuery_Handler");
                }

                    #endregion                   
	}
}
