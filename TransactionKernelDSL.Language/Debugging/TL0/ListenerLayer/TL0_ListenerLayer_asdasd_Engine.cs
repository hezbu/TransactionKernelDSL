
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using TransactionKernelDSL.Framework.Parser.Iso8583;

namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// A transactional engine named asdasd
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:10
    /// </remarks>
public partial class TL0_ListenerLayer_asdasd_Engine: AbstractTcpTriggeredMultiThreadedInputTransactionEngine
	{

        
    
        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        #region Static fields
        protected static TL0_ListenerLayer_asdasd_Engine  _Instance = null;
        #endregion

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        public static TL0_ListenerLayer_asdasd_Engine  Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TL0_ListenerLayer_asdasd_Engine();
                }

                return _Instance;
            }
        }

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        private TL0_ListenerLayer_asdasd_Engine()
            : base()
        {
            this.Logger = "MainLogger";
        }
                /// <summary>
        /// TRANSFORMACION - con los datos obtenidos de los handler implementados en el DSL
        /// </summary>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        protected override AbstractTransactionHandler TransactionHandlerFactory(object transactionId)
        {
            switch(((string)transactionId))
            {
                                        case "1": 
                            return new TL0_ListenerLayer_TransactionName1_Handler(); 

              
                default:
                    throw new ApplicationException("Transaction ID '" + transactionId + "' unknown!");
            }
        }
        protected override AbstractTransactionParser ParserFactory(object state = null) 
{ 
Iso8583Parser parser =  new Iso8583Parser("1"); 
parser.Logger = "MainLogger"; 
return parser; 
} 
	}
}

	
    