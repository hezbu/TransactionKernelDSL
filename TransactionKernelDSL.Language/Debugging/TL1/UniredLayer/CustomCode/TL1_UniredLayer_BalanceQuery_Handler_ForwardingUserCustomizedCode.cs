
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
    /// Generated on 06/05/2016 14:29:59
    /// </remarks>
    public partial class TL1_UniredLayer_BalanceQuery_Handler: AbstractTransactionHandler,ITransactionHandlerForwardable
	{    
                #region ITransactionHandlerForwardable User-Customized Members

                public bool BuildRequirement_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return BuildRequirement();

                    throw new NotImplementedException("Implementation left for BuildRequirement_UC() in TL1_UniredLayer_BalanceQuery_Handler");
                }                       

                public bool Resolve_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return Resolve();

                   throw new NotImplementedException("Implementation left for Resolve_UC() in TL1_UniredLayer_BalanceQuery_Handler");
                }
                      
                public bool GetResponse_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return GetResponse();

                    throw new NotImplementedException("Implementation left for GetResponse_UC() in TL1_UniredLayer_BalanceQuery_Handler");
                }

                public bool ForwardHandlerFactory_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return ForwardHandlerFactory();

                    throw new NotImplementedException("Implementation left for ForwardHandlerFactory_UC() in TL1_UniredLayer_BalanceQuery_Handler");
                }

                public bool ProcessTransaction_UC()
                {
                     // Uncomment this line if you want to use default behaviour.
                    // return ProcessTransaction();

                        throw new NotImplementedException("Implementation left for ProcessTransaction_UC() in TL1_UniredLayer_BalanceQuery_Handler");
                }

                    #endregion                   
	}
}
