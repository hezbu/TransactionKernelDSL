
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using System.Globalization;

namespace PDS.Switch.CorrBanc
{

    /// <summary>
    /// A transaction handler named Consulta de Saldo with Id  0100999999
    /// </summary>
    /// <remarks>
    /// Generated on 17/4/2016 11:19:17
    /// </remarks>
	public partial class TL0_ListenerLayer_ConsultadeSaldo_Handler: AbstractTransactionHandler,ITransactionHandlerListenable
	{
         public  TL0_ListenerLayer_ConsultadeSaldo_Handler()
            : base()
        {

            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            this.Logger = "MainLogger";
                _Context = new CorrBancContext();
                   _Context[CorrBancFacade.ConstStan] = CorrBancFacade.Instance.SequenceFactory() as string;
                    if (String.IsNullOrEmpty(_Context[CorrBancFacade.ConstStan]) == false)
                    {
                        _Log.Info("Sequence number is " + _Context[CorrBancFacade.ConstStan]);
                    }
                    else
                    {      
                        _Log.Error("Couldn't get sequence number for handler TL0_ListenerLayer_ConsultadeSaldo_Handler ");
                    }
                                            this.GetRequirementMethod = new GetRequirementDelegate(GetRequirement_UC);
                        this.BuildResponseMethod = new BuildResponseDelegate(BuildResponse_UC);
                        this.ReplyMethod = new ReplyDelegate(Reply_UC);
  
         
        }

                                #region ITransactionHandlerListenable Members
                
                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method must be implemented in order to retrieve and validate data from an AbstractTransactionParserStructure,
                        /// within internal AbstractTransactionParser object, passed by the input engine. Once data is validated, it shall be
                        /// saved on an valid AbstractTransactionContext object within this transaction, so it can be shared between other methods and layers.
                        /// </summary>
                        /// <returns>TRUE if data was retrieved and validated OK, otherwise FALSE</returns>
                        public bool GetRequirement()
                        {
                            throw new NotImplementedException("Implementation left for GetRequirement() in TL0_ListenerLayer_ConsultadeSaldo_Handler");
                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method must be implemented in order to build a valid response from an AbstractTransactionContext object within
                        /// this transaction, to an AbstractTransactionParserStructure within the internal Parser object.
                        /// </summary>
                        /// <returns>TRUE if a valid response was built, otherwise FALSE</returns>
                        public bool BuildResponse()
                        {
                            throw new NotImplementedException("Implementation left for BuildResponse() in TL0_ListenerLayer_ConsultadeSaldo_Handler");
                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method must be implemented in order to eventually send the response to the connected client. This method
                        /// has the final decission regarding to whether sending or not a reply, checking internal statuses or values from upper layers, for instance.
                        /// </summary>
                        /// <returns>TRUE if a reply coudl be sent, otherwise FALSE</returns>
                        public bool Reply()
                        {
                            if (_AssignedParser.SendMethod != null && _AssignedParser.SendMethod(_AssignedClient) == true) 
{ return true;  } 
 else 
{ 
_Log.Error("Error found during Reply() stage: " + _AssignedParser.ErrorMessage + " (" + (_AssignedClient as System.Net.Sockets.TcpClient).Client.LocalEndPoint.ToString() + " - " + (_AssignedClient as System.Net.Sockets.TcpClient).Client.RemoteEndPoint.ToString() + ")"); 
return false; 
} 

                        }

                        #endregion
                                    
	}
}
