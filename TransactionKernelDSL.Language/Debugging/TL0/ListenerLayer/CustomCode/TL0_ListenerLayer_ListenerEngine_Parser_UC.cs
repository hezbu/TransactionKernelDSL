
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
                        /// A transactional parser named TL0_ListenerLayer_ListenerEngine_Parser
                        /// </summary>
                        /// <remarks>
                        /// Generated on 06/05/2016 14:30:06
                        /// </remarks>
                        public partial class TL0_ListenerLayer_ListenerEngine_Parser: AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
	                    {
                            private void SetupParser_UC()
                            {
                                //Herein, should be defined the parser's nickname. It will be used in IN and OUT logs (i.e XXX_IN , XXX_OUT).
                                //this._ParserNickName = "MyParserName"

                            
                                throw new NotImplementedException("Please define any parser's user setup here, or erase this exception");

                                return;
                            }

                            #region ITransactionParserCommunicable Members

                            public bool Send_UC(object handler)
                            {
                                // Uncomment this line for default behaviour
                                //return this.Send(handler);

                                throw new NotImplementedException("Send_UC must be implemented");
                            }

                            public bool Receive_UC(object handler)
                            {
                                // Uncomment this line for default behaviour
                                // return this.Receive(handler);

                                throw new NotImplementedException("Receive_UC must be implemented");
                            }

                            public bool IsKeepAliveMessage_UC()
                            {
                                // Uncomment this line for default behaviour
                                // return this.IsKeepAliveMessage();

                                throw new NotImplementedException("IsKeepAliveMessage_UC must be implemented");
                            }

                            #endregion

                            #region ITransactionParserAssembleable Members

                            public bool Assemble_UC()
                            {
                                // Uncomment this line for default behaviour
                                // return this.Assemble();

                                throw new NotImplementedException("Assemble_UC must be implemented");
                            }

                            public bool Disassemble_UC()
                            {
                                // Uncomment this line for default behaviour
                                // return this.Disassemble();

                                throw new NotImplementedException("Disassemble_UC must be implemented");
                            }
                        }
                    }
