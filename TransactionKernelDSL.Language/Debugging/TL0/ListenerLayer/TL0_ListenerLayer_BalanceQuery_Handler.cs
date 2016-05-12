
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using System.Globalization;

namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// A transaction handler named Balance Query  with Id  3100000010
    /// </summary>
    /// <remarks>
    /// Generated on 06/05/2016 15:16:54
    /// </remarks>
	public partial class TL0_ListenerLayer_BalanceQuery_Handler: AbstractTransactionHandler,ITransactionHandlerListenable,ITransactionHandlerForwardable
	{
         public  TL0_ListenerLayer_BalanceQuery_Handler()
            : base()
        {

            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            this.Logger = "MainLogger";
                _Context = new PDSNetContext();
                   _Context[PDSNetFacade.ConstStan] = PDSNetFacade.Instance.SequenceFactory() as string;
                    if (String.IsNullOrEmpty(_Context[PDSNetFacade.ConstStan]) == false)
                    {
                        _Log.Info("Sequence number is " + _Context[PDSNetFacade.ConstStan]);
                    }
                    else
                    {      
                        _Log.Error("Couldn't get sequence number for handler TL0_ListenerLayer_BalanceQuery_Handler ");
                    }
                                            this.GetRequirementMethod = new GetRequirementDelegate(GetRequirement_UC);
                        this.BuildResponseMethod = new BuildResponseDelegate(BuildResponse_UC);
                        this.ReplyMethod = new ReplyDelegate(Reply_UC);
            this.ForwardHandlerFactoryMethod = new ForwardHandlerFactoryDelegate(ForwardHandlerFactory_UC);
            this.GetResponseMethod = new GetResponseDelegate(GetResponse);
            this.BuildRequirementMethod = new BuildRequirementDelegate(BuildRequirement);
            this.ResolveMethod = new ResolveDelegate(Resolve);
            this.ProcessTransactionMethod = new ProcessTransactionDelegate(ProcessTransaction);
   
         
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
                            throw new NotImplementedException("Implementation left for GetRequirement() in TL0_ListenerLayer_BalanceQuery_Handler");
                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method must be implemented in order to build a valid response from an AbstractTransactionContext object within
                        /// this transaction, to an AbstractTransactionParserStructure within the internal Parser object.
                        /// </summary>
                        /// <returns>TRUE if a valid response was built, otherwise FALSE</returns>
                        public bool BuildResponse()
                        {
                            throw new NotImplementedException("Implementation left for BuildResponse() in TL0_ListenerLayer_BalanceQuery_Handler");
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
                                             #region ITransactionHandlerForwardable Members

                        /// <summary>                        
                        /// This methods has the responsability of creating a requirement context, likely in a Request AbstractParserStructure,
                        /// in order to be sent to another layer or extern component.
                        /// </summary>
                        /// <returns>TRUE if it was possible to create a requirement, otherwise FALSE</returns>
                        public bool BuildRequirement()
                        {                
                            return ( this._ForwardHandler.BuildRequirementMethod == null || this._ForwardHandler.BuildRequirementMethod() == true);
                        }              
         
                        /// <summary>                       
                        /// This method has the responsability to eventually send from a Request AbstractParserStructure (or another type of context) and/or receive in a Response AbstractParserStructure (or another type of context),
                        /// in order to exchange data between handlers or extern components.
                        /// </summary>
                        /// <returns>TRUE if it was possible to exchange information without errors, otherwise FALSE</returns>
                        public bool Resolve()
                        {
                            return ( this._ForwardHandler.ResolveMethod == null || this._ForwardHandler.ResolveMethod() == true);                            
                        }

                        /// <summary>                      
                        /// This methods is executed whenever Resolve() has returned TRUE, in order to evaluate if the response is whether OK or not,
                        /// at a business-logical-decission level.
                        /// </summary>
                        /// <returns>TRUE if response is OK at business-logical-decission level, otherwise FALSE</returns>
                        public bool GetResponse()
                        {                 
                            return( this._ForwardHandler.GetResponseMethod == null || this._ForwardHandler.GetResponseMethod() == true);                            
                        }

                        /// <summary>                     
                        /// Must be implemented in order to instanciate a valid forwarding handler object in ForwardingHandler member.
                        /// </summary>
                        /// <returns>TRUE, if it was possible to instanciate the objet, otherwise FALSE</returns>
                        public bool ForwardHandlerFactory()
                        {
                               throw new NotImplementedException("Implementation left for ForwardHandlerFactory() in TL0_ListenerLayer_BalanceQuery_Handler");
                        }

                        /// <summary>                       
                        /// This method is always executed after the other forwarding stage methods, and should be implemented in order
                        /// to analyse any type of either expected or unexpected transaction's status after this stage.
                        /// </summary>
                        /// <returns>TRUE if everything is OK, otherwise FALSE</returns>
                        public bool ProcessTransaction()
                        {
                            return ( this._ForwardHandler.ProcessTransactionMethod == null || this._ForwardHandler.ProcessTransactionMethod() == true);                            
                        }
                    #endregion
                            
	}
}
	