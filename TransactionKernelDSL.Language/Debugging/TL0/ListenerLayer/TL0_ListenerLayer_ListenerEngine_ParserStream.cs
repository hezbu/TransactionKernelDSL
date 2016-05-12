
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
                        /// A transactional parser stream named TL0_ListenerLayer_ListenerEngine_ParserStream
                        /// </summary>
                        /// <remarks>
                        /// Generated on 06/05/2016 15:16:55
                        /// </remarks>
                        public partial class TL0_ListenerLayer_ListenerEngine_ParserStream: AbstractTransactionParserStream
	                    {
         
                            public TL0_ListenerLayer_ListenerEngine_ParserStream()
                                : base(8192)
                            {
                                this.SetupStream_UC();
                            }
                        }
                    }
