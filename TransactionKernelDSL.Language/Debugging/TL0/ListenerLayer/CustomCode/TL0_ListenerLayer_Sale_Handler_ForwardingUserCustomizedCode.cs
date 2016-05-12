
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
    public partial class TL0_ListenerLayer_Sale_Handler: AbstractTransactionHandler,ITransactionHandlerListenable,ITransactionHandlerForwardable
	{    
                #region ITransactionHandlerForwardable User-Customized Members              

                public bool ForwardHandlerFactory_UC()
                {
                    // Uncomment this line if you want to use default behaviour.
                    // return ForwardHandlerFactory();

                    throw new NotImplementedException("Implementation left for ForwardHandlerFactory_UC() in TL0_ListenerLayer_Sale_Handler");
                }
              

                    #endregion                   
	}
}
