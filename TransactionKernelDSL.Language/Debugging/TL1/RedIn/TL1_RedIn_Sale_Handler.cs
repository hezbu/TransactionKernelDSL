
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
    /// A transaction handler named Sale with Id  V
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:11
    /// </remarks>
	public partial class TL1_RedIn_Sale_Handler: AbstractTransactionHandler,ITransactionHandlerForwardable
	{
         public  TL1_RedIn_Sale_Handler(AbstractTransactionContext context)
            : base(context)
        {

            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            this.Logger = "MainLogger";
            
            this.ForwardHandlerFactoryMethod = new ForwardHandlerFactoryDelegate(ForwardHandlerFactory_UC);
            this.GetResponseMethod = new GetResponseDelegate(GetResponse_UC);
            this.BuildRequirementMethod = new BuildRequirementDelegate(BuildRequirement_UC);
            this.ResolveMethod = new ResolveDelegate(Resolve_UC);
            this.ProcessTransactionMethod = new ProcessTransactionDelegate(ProcessTransaction_UC);
   
         
        }

        
                     #region ITransactionHandlerForwardable Members

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method has the responsability of creating a requirement context, likely in a Request AbstractParserStructure,
                        /// in order to be sent to another layer or extern component.
                        /// </summary>
                        /// <returns>TRUE if it was possible to create a requirement, otherwise FALSE</returns>
                        public bool BuildRequirement()
                        {               
                            throw new NotImplementedException("Implementation left for BuildRequirement() in TL1_RedIn_Sale_Handler");
                        } 

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method has the responsability to eventually send from a Request AbstractParserStructure (or another type of context) and/or receive in a Response AbstractParserStructure (or another type of context),
                        /// in order to exchange data between handlers or extern components.
                        /// </summary>
                        /// <returns>TRUE if it was possible to exchange information without errors, otherwise FALSE</returns>
                        public bool Resolve()
                        {
                            _Context.TransmissionStatus = TL1_RedIn_RedinEngine_Engine.Instance.Resolve(_AssignedParser);
                            return _Context.TransmissionStatus == TransmissionStatus.NoError; 
                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method is executed whenever Resolve() has returned TRUE, in order to evaluate if the response is whether OK or not,
                        /// at a business-logical-decission level.
                        /// </summary>
                        /// <returns>TRUE if response is OK at business-logical-decission level, otherwise FALSE</returns>
                        public bool GetResponse()
                        {
                             throw new NotImplementedException("Implementation left for GetResponse() in TL1_RedIn_Sale_Handler");
                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// Must be implemented in order to instanciate a valid forwarding handler object in ForwardingHandler member.
                        /// </summary>
                        /// <returns>TRUE, if it was possible to instanciate the objet, otherwise FALSE</returns>
                        public bool ForwardHandlerFactory()
                        {
                                                            return true; //Nothing to do, no more further forward steps to instance.  
                                                        }

                        /// <summary>
                        /// [NOT USED - CUSTOM CODE IS LINKED IN CONSTRUCTOR]
                        /// This method is always executed after the other forwarding stage methods, and should be implemented in order
                        /// to analyse any type of either expected or unexpected transaction's status after this stage.
                        /// </summary>
                        /// <returns>TRUE if everything is OK, otherwise FALSE</returns>
                        public bool ProcessTransaction()
                        {
                            throw new NotImplementedException("Implementation left for ProcessTransaction() in TL1_RedIn_Sale_Handler");
                        }
                    #endregion
            
	}
}
	