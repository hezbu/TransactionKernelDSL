
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
    public partial class TL0_ListenerLayer_BalanceQuery_Handler: AbstractTransactionHandler,ITransactionHandlerListenable,ITransactionHandlerForwardable
	{    
                #region ITransactionHandlerForwardable User-Customized Members              

                public bool ForwardHandlerFactory_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return ForwardHandlerFactory();

                    throw new NotImplementedException("Implementation left for ForwardHandlerFactory_UC() in TL0_ListenerLayer_BalanceQuery_Handler");
                }
              

                    #endregion                   
	}
}
