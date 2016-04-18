
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using TransactionKernelDSL.Framework.Parser.Iso8583;

namespace PDS.Switch.CorrBanc
{

    /// <summary>
    /// A transactional engine named Listener Engine
    /// </summary>
    /// <remarks>
    /// Generated on 17/4/2016 11:19:16
    /// </remarks>
public partial class TL0_ListenerLayer_ListenerEngine_Engine: AbstractTcpTriggeredMultiThreadedInputTransactionEngine
	{

        
    
        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        #region Static fields
        protected static TL0_ListenerLayer_ListenerEngine_Engine  _Instance = null;
        #endregion

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        public static TL0_ListenerLayer_ListenerEngine_Engine  Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TL0_ListenerLayer_ListenerEngine_Engine();
                }

                return _Instance;
            }
        }

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        private TL0_ListenerLayer_ListenerEngine_Engine()
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
                                        case "0100999999": 
                            return new TL0_ListenerLayer_ConsultadeSaldo_Handler(); 

              
                default:
                    throw new ApplicationException("Transaction ID '" + transactionId + "' unknown!");
            }
        }
        protected override AbstractTransactionParser ParserFactory(object state = null) 
{ 
Iso8583Parser parser =  new Iso8583Parser("ISO"); 
parser.Logger = "MainLogger"; 
return parser; 
} 
	}
}

