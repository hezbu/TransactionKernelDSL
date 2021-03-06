
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
    /// A transactional engine named Redin Engine
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:10
    /// </remarks>
public partial class TL1_RedIn_RedinEngine_Engine: AbstractOutputTransactionEngine

	{

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        #region Static fields
        protected static TL1_RedIn_RedinEngine_Engine  _Instance = null;
        #endregion

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        public static TL1_RedIn_RedinEngine_Engine  Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new TL1_RedIn_RedinEngine_Engine();
                }

                return _Instance;
            }
        }
         /// <summary>
        /// TRANSFORMACION
        /// </summary>
        private TL1_RedIn_RedinEngine_Engine()
            : base()
        {
            this.Logger = "MainLogger";
            throw new NotImplementedException("Parser must be initialized in engine's ctor");
        }

	}
}

