using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Http
{
    public class HttpParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public HttpParser(string rootSection = "Xml", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStructure = new XmlRequestStructure();
            this._ResponseStructure = new XmlResponseStructure();

            this._RequestStream = new XmlStream();
            this._ResponseStream = new XmlStream();
        }

        #region ITransactionParserCommunicable Members
        public bool Send(object handler)
        {
            try
            {
                string package = (ResponseStream as XmlStream).Get();
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(AbstractTransactionFacade.GetBytes(package), 0, AbstractTransactionFacade.GetBytes(package).Length);
                    _Log.Info(_RootSection + "_OUT: " + ((XmlStream)ResponseStream).ToString());
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
            int bytesToRead = HttpStream.HttpStreamMaxLength;
            byte[] btPartialReadsBuffer = new byte[HttpStream.HttpStreamMaxLength];
            byte[] btAccumulatedReadBuffer = new byte[HttpStream.HttpStreamMaxLength];


            try
            {
                while (true)
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 1);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;

                    for (int i = 0; i < totalBytesRead; i++)
                    {
                        if (btAccumulatedReadBuffer[i] == 0x0D &&
                            btAccumulatedReadBuffer[i + 1] == 0x0A &&
                            btAccumulatedReadBuffer[i + 2] == 0x0D &&
                            btAccumulatedReadBuffer[i + 3] == 0x0A
                            )
                        {
                            break;
                        }
                    }
                     
                }
                //blocks until a client sends a message
                for (totalBytesRead = 0; (totalBytesRead < bytesToRead); )
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, bytesToRead - totalBytesRead);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;



                    if (bytesRead == 0) ///No se leyo nada
                    {
                        this._ErrorMessage = "Error leyendo datos de conexion: bytesRead = 0 ";
                        //  _Log.Error(this._ErrorMessage);
                        this._Status |= TransmissionStatus.ContactLost;
                        return false;
                    }
                    else
                    {
                        var endTest = AbstractTransactionFacade.GetString(btAccumulatedReadBuffer, totalBytesRead);


                    }
                }

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

            _RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead);

            if (this.IsKeepAliveMessage() == false)
                _Log.Info(_RootSection + "_IN: " + ((HttpStream)RequestStream).ToString());

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
