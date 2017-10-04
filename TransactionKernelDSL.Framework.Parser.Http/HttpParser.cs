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
        public HttpParser(string rootSection = "Http", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStream = new HttpStream();
            this._ResponseStream = new HttpStream();

            this._RequestStructure = new HttpStructure();
            this._ResponseStructure = new HttpStructure();
        }

        #region ITransactionParserCommunicable Members
        public bool Send(object handler)
        {
            try
            {
                string package = (ResponseStream as HttpStream).Get();
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(AbstractTransactionFacade.GetBytes(package), 0, AbstractTransactionFacade.GetBytes(package).Length);
                    _Log.Info(_RootSection + "_OUT: " + ((HttpStream)ResponseStream).ToString());
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
            string header = null;
            bool isRnEnding = true;

            try
            {
                var headerFound = false;
                while (headerFound == false)
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 1);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;

                    if (bytesRead == 0) ///No se leyo nada
                    {
                        this._ErrorMessage = "Error leyendo datos de conexion: bytesRead = 0 ";
                        //  _Log.Error(this._ErrorMessage);
                        this._Status |= TransmissionStatus.ContactLost;
                        return false;
                    }


                    if (totalBytesRead > 4)
                    {
                        if (btAccumulatedReadBuffer[totalBytesRead - 4] == 0x0D &&
                       btAccumulatedReadBuffer[totalBytesRead - 3] == 0x0A &&
                       btAccumulatedReadBuffer[totalBytesRead - 2] == 0x0D &&
                       btAccumulatedReadBuffer[totalBytesRead - 1] == 0x0A
                       )
                        {
                            header = AbstractTransactionFacade.GetString(btAccumulatedReadBuffer, totalBytesRead);

                            headerFound = true;
                            break;
                        }
                    }

                    if (totalBytesRead > 2)
                    {
                        if (btAccumulatedReadBuffer[totalBytesRead - 2] == 0x0A &&
                              btAccumulatedReadBuffer[totalBytesRead - 1] == 0x0A
                       )
                        {
                            header = AbstractTransactionFacade.GetString(btAccumulatedReadBuffer, totalBytesRead);
                            isRnEnding = false;
                            headerFound = true;
                            break;
                        }
                    }




                }
                if (header != null)
                {
                    if (header.Contains("Content-Length:") == true)
                    {

                        var startIndex = header.IndexOf("Content-Length:") + "Content-Length:".Length;
                        var endIndex = 0;


                       
                            endIndex = header.IndexOf("\n", startIndex);
                       
                      


                        var contentLength = header.Substring(startIndex, endIndex - startIndex).Trim();

                        //blocks until a client sends a message
                        for (totalBytesRead = header.Length; (totalBytesRead < (int.Parse(contentLength)) + header.Length); )
                        {
                            bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, int.Parse(contentLength) - (totalBytesRead - header.Length));
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
                    }
                    else
                    {
                        _RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead);

                        if (this.IsKeepAliveMessage() == false)
                            _Log.Info(_RootSection + "_IN: " + ((HttpStream)RequestStream).ToString());

                        return true;
                    }
                }


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
                _Log.ErrorFormat("Excepcion encontrada: {0} - Array era: {1}", ex, AbstractTransactionFacade.GetString(btAccumulatedReadBuffer, totalBytesRead));
                this._ErrorMessage = String.Format("TIMEOUT antes de determinar el proceso (handler) que requiere la conexión. Mensaje: {0}", ex.Message);

                this._Status |= TransmissionStatus.Timeout;
                return false;
            }


            _Log.InfoFormat("DUMP_IN: {0}", AbstractTransactionFacade.Dump(btAccumulatedReadBuffer, totalBytesRead, 0));
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
