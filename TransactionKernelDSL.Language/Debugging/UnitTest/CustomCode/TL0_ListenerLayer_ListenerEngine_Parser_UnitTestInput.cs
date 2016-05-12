
                    using System;
	                using System.Collections.Generic;
	                using System.Text;
                    using TransactionKernelDSL.Framework.V1;
                    using log4net.Config;
                    using log4net;
                    using System.Threading;
                    using NUnit.Framework;
                    #if DEBUG == true
                    namespace PDS.Switch.PDSNet
                    {

                        /// <summary>
                        /// A transactional unit test input named TL0_ListenerLayer_ListenerEngine_Parser_UnitTestInput
                        /// </summary>
                        /// <remarks>
                        /// Generated on 06/05/2016 15:14:30
                        /// </remarks>                        
                        public partial class TL0_ListenerLayer_ListenerEngine_Parser_UnitTestInput
	                    {
                           
                            public static TL0_ListenerLayer_ListenerEngine_ParserStructure GetResponseStructure()
                            {
                                    TL0_ListenerLayer_ListenerEngine_ParserStructure respSt = new TL0_ListenerLayer_ListenerEngine_ParserStructure();
                                    throw new NotImplementedException("It is mandatory to fill up a Response Structure for this test");
                                    return respSt;
                            }

                            public static TL0_ListenerLayer_ListenerEngine_ParserStream GetRequestStream()
                            {
                                    TL0_ListenerLayer_ListenerEngine_ParserStream reqSt = new TL0_ListenerLayer_ListenerEngine_ParserStream();
                                    throw new NotImplementedException("It is mandatory to fill up a Request Stream for this test");
                                    return reqSt;
                            }
                            
                        }
                    }
                    #endif
