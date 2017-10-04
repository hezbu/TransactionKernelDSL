using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.NoProtocol
{
    public class NoProtocolParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public NoProtocolParser(string rootSection = "NoProtocol", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStructure = new NoProtocolParserStructure();
            this._ResponseStructure = new NoProtocolParserStructure();


            this._RequestStream = new NoProtocolParserStream();
            this._ResponseStream = new NoProtocolParserStream();
        }

        #region ITransactionParserCommunicable Members
        public bool Send(object handler)
        {
            try
            {
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    var lengthToSend = ResponseStream.Get().Length;

                    var test1 = new byte[lengthToSend - 16];
                    var test2 = new byte[16];

                    Buffer.BlockCopy(ResponseStream.Get(), 0, test1, 0, test1.Length);
                    Buffer.BlockCopy(ResponseStream.Get(), test1.Length, test2, 0, test2.Length);


                    ((TcpClient)handler).GetStream().Write(test1, 0, test1.Length);
                    Thread.Sleep(2000);
                    ((TcpClient)handler).GetStream().Write(test2, 0, test2.Length);


                    //((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, ResponseStream.Get().Length);
                    //_Log.InfoFormat("{0}_OUT: {2}{1}", _RootSection, DumpHelper.Dump(ResponseStream.Get(), ResponseStream.Get().Length), Environment.NewLine);
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
                _Log.Error(this._ErrorMessage);
                _Status |= TransmissionStatus.SendingError;
                return false;
            }
        }

        public bool Receive(object handler)
        {
            int bytesRead = 0;
            int totalBytesRead = 0;
            byte[] headerArr = new byte[4 + 1];
            int bytesToRead = NoProtocolParserStream.NoProtocolStreamMaxLength;
            byte[] btPartialReadsBuffer = new byte[NoProtocolParserStream.NoProtocolStreamMaxLength];
            byte[] btAccumulatedReadBuffer = new byte[NoProtocolParserStream.NoProtocolStreamMaxLength];


            try
            {
                bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, bytesToRead);




                //break;
            }
            catch (IOException ioEx)
            {
                if (ioEx.InnerException != null && ioEx.InnerException.GetType().Name == "SocketException")
                {
                    switch ((ioEx.InnerException as SocketException).ErrorCode)
                    {
                        case 0x2746:
                            this._Status |= TransmissionStatus.ContactLost;
                            break;
                        case 0x274C:
                            this._Status |= TransmissionStatus.Timeout;
                            break;
                        default:
                            _Log.ErrorFormat("Error at Receive: SocketException not recognized: {0}", (ioEx.InnerException as SocketException).ErrorCode);
                            this._Status |= TransmissionStatus.ConnectionError;
                            break;
                    }

                }
                else
                {
                    _Log.ErrorFormat("Error at Receive: Exception found {0}", ioEx);
                }

                return false;
            }
            catch (Exception ex)
            {
                this._ErrorMessage = "TIMEOUT antes de determinar el proceso (handler) que requiere la conexión. Mensaje: " + ex.Message;

                this._Status |= TransmissionStatus.Timeout;
                return false;
            }






            _RequestStream.Set(btPartialReadsBuffer, bytesRead);
           
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
            return true;
        }

        public bool Disassemble()
        {
            return true;
        }
        #endregion
    }
}
