
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
    /// User-customized helpers for Listenable stage methods 
    /// </summary>
    /// <remarks>
    /// Generated on 06/05/2016 14:30:01
    /// </remarks>
	public partial class TL0_ListenerLayer_BalanceQuery_Handler: AbstractTransactionHandler,ITransactionHandlerListenable,ITransactionHandlerForwardable
	{    
                                                     #region ITransactionHandlerListenable User-Customized Members
                                /// <summary>                 
                                /// This method must be implemented in order to retrieve and validate data from an AbstractTransactionParserStructure,
                                /// within internal AbstractTransactionParser object, passed by the input engine. Once data is validated, it shall be
                                /// saved on an valid AbstractTransactionContext object within this transaction, so it can be shared between other methods and layers.
                                /// </summary>
                                /// <returns>TRUE if data was retrieved and validated OK, otherwise FALSE</returns>
                                public bool GetRequirement_UC()
                                {
                                    // Uncomment this line if you want to use default behaviour.
                                    // return GetRequirement();

                                    throw new NotImplementedException("Implementation left for GetRequirement_UC() in TL0_ListenerLayer_BalanceQuery_Handler");
                                }
                                /// <summary>                   
                                /// This method must be implemented in order to build a valid response from an AbstractTransactionContext object within
                                /// this transaction, to an AbstractTransactionParserStructure within the internal Parser object.
                                /// </summary>
                                /// <returns>TRUE if a valid response was built, otherwise FALSE</returns>
                                public bool BuildResponse_UC()
                                {
                                    // Uncomment this line if you want to use default behaviour.
                                    // return BuildResponse();

                                    throw new NotImplementedException("Implementation left for BuildResponse_UC() in TL0_ListenerLayer_BalanceQuery_Handler");
                                }

                                /// <summary>                   
                                /// This method must be implemented in order to eventually send the response to the connected client. This method
                                /// has the final decission regarding to whether sending or not a reply, checking internal statuses or values from upper layers, for instance.
                                /// </summary>
                                /// <returns>TRUE if a reply coudl be sent, otherwise FALSE</returns>
                                public bool Reply_UC()
                                {
                                    // Uncomment this line if you want to use default behaviour.
                                    // return Reply();

                                    throw new NotImplementedException("Implementation left for Reply_UC() in TL0_ListenerLayer_BalanceQuery_Handler");
                                }
                                 #endregion     
                   

                                  
	}
}
