
                    using System;
	                using System.Collections.Generic;
	                using System.Text;
                    using TransactionKernelDSL.Framework.V1;
                    using log4net.Config;
                    using log4net;
                    using System.Threading;
                    using NUnit.Framework;
					using System.Diagnostics;
                    #if DEBUG == true
                    namespace PDS.Switch.PDSNet
                    {

                        /// <summary>
                        /// A transactional unit test class named TL0_ListenerLayer_ListenerEngine_Parser_UnitTest
                        /// </summary>
                        /// <remarks>
                        /// Generated on 06/05/2016 15:16:56
                        /// </remarks>
                        [TestFixture]
                        public partial class TL0_ListenerLayer_ListenerEngine_Parser_UnitTest
	                    {
                            [Test]
                            public void TL0_ListenerLayer_ListenerEngine_Parser_UnitTest_Assemble_Test()
                            {
                                    
                                    TL0_ListenerLayer_ListenerEngine_Parser parser = new TL0_ListenerLayer_ListenerEngine_Parser();

                                    parser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_Parser_UnitTestInput.GetResponseStructure();
                                    Assert.True(parser.AssembleMethod());
                                    Debug.WriteLine(parser.ResponseStream.ToString());
                            }

                            [Test]
                            public void TL0_ListenerLayer_ListenerEngine_Parser_UnitTest_Disassemble_Test()
                            {
                                     TL0_ListenerLayer_ListenerEngine_Parser parser = new TL0_ListenerLayer_ListenerEngine_Parser();
                                    
                                    parser.RequestStream = TL0_ListenerLayer_ListenerEngine_Parser_UnitTestInput.GetRequestStream();
                                    Assert.True(parser.DisassembleMethod());
                                    Debug.WriteLine(parser.RequestStructure.ToString());
                            }
                        }
                    }
                    #endif
