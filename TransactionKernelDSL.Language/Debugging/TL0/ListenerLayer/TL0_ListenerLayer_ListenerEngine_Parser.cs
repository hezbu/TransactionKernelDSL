
                    using System;
	                using System.Collections.Generic;
	                using System.Text;
                    using TransactionKernelDSL.Framework.V1;
                    using log4net.Config;
                    using log4net;
                    using System.Threading;
                    using System.Net.Sockets;
                    namespace PDS.Switch.PDSNet
                    {

                        /// <summary>
                        /// A transactional parser named TL0_ListenerLayer_ListenerEngine_Parser
                        /// </summary>
                        /// <remarks>
                        /// Generated on 06/05/2016 15:16:55
                        /// </remarks>
                        public partial class TL0_ListenerLayer_ListenerEngine_Parser: AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
	                    {
                              private string _ParserNickName = "PP";

                              public TL0_ListenerLayer_ListenerEngine_Parser()
                                    : base(null, false)
                                {
                                    this._AssembleMethod = Assemble_UC;
                                    this._DisassembleMethod = Disassemble_UC;

                                    this._SendMethod = Send_UC;
                                    this._ReceiveMethod = Receive_UC;
                                    this._IsKeepAliveMessageMethod = IsKeepAliveMessage_UC;

                                    this._RequestStructure = new TL0_ListenerLayer_ListenerEngine_ParserStructure();
                                    this._ResponseStructure = new TL0_ListenerLayer_ListenerEngine_ParserStructure();
                                    this._RequestStream = new TL0_ListenerLayer_ListenerEngine_ParserStream();
                                    this._ResponseStream = new TL0_ListenerLayer_ListenerEngine_ParserStream();

                                    this.SetupParser_UC();

                                }

                            #region ITransactionParserCommunicable Members

                            public bool Send(object handler)
                            {
                                try
                                {
                                    if (IsKeepAliveMessage() == false)
                                    {
                                        ((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, ResponseStream.Get().Length);
                                        _Log.Info(this._ParserNickName+"_OUT: " + ResponseStream.ToString());
                                    }
                                    else
                                    {
                                        ((TcpClient)handler).GetStream().Write(new byte[] { 0x06 }, 0, 1);
                                    }

                                    return true;
                                }
                                catch (Exception ee)
                                {
                                    _ErrorMessage = "Error enviando: " + ee.Message;
                                    _Status |= TransmissionStatus.SendingError;
                                    return false;
                                }
                            }

                            public bool Receive(object handler)
                            {
                                int bytesRead;
                                int totalBytesRead = 0;          
                                byte[] accumulatedBytesRead = new byte[8192];

                                DateTime dtStart = System.DateTime.Now;
                                bytesRead = 0;


                                try
                                {
                                    bytesRead = ((TcpClient)handler).GetStream().Read(accumulatedBytesRead, totalBytesRead, 8192 - totalBytesRead);
                                    totalBytesRead += bytesRead;

                                    if (bytesRead == 0)
                                    {
                                        _ErrorMessage = "Error leyendo datos de conexion: bytesRead = 0";
                                        _Status |= TransmissionStatus.ContactLost;
                                        return false;
                                    }

                                }
                                catch (System.IO.IOException ioEx)
                                {
                                    DateTime dtUnexpectedEnd = System.DateTime.Now;
                                    _ErrorMessage = ioEx.Message;
                                    _Status |= TransmissionStatus.ProblemDuringContact;
                                    return false;
                                }
                                catch (Exception ex)
                                {
                                    //a socket error has occured o fue un timeout
                                    _ErrorMessage = ex.Message;
                                    _Status |= TransmissionStatus.Timeout;

                                    return false;
                                }



                                this.RequestStream.Set(accumulatedBytesRead, totalBytesRead);
                                _Log.Info(this._ParserNickName+"_IN: " + RequestStream.ToString());
                                return true;
                            }

                            public bool IsKeepAliveMessage()
                            {
                                return false;
                            }

                            #endregion

                            #region ITransactionParserAssembleable Members

                            public bool Assemble()
                            {
                                throw new NotImplementedException();
                            }

                            public bool Disassemble()
                            {
                                throw new NotImplementedException();
                            }

                            #endregion
                        }
                    }
