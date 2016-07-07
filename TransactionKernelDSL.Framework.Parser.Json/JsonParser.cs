using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Json
{
    public class JsonParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public JsonParser(string rootSection = "Json", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStructure = new JsonParserRequestStructure( _RootSection);
            this._ResponseStructure = new JsonParserResponseStructure( _RootSection);

            this._RequestStream = new JsonParserStream();
            this._ResponseStream = new JsonParserStream();
        }

        #region ITransactionParserCommunicable Members
        public bool Send(object handler)
        {
            try
            {
                string package = (ResponseStream as JsonParserStream).Get();
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(AbstractTransactionFacade.GetBytes(package), 0, AbstractTransactionFacade.GetBytes(package).Length);
                    var stub = JsonConvert.DeserializeObject(((JsonParserStream)ResponseStream).ToString().Substring(4));

                    //_Log.InfoFormat("{0}_OUT: {1}", _RootSection, ((JsonParserStream)ResponseStream).ToString());
                    _Log.InfoFormat("{0}_OUT: {2}{1}", _RootSection, JsonConvert.SerializeObject(stub, Formatting.Indented), Environment.NewLine);
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
            int bytesToRead = JsonParserStream.JsonStreamMaxLength;
            byte[] btPartialReadsBuffer = new byte[JsonParserStream.JsonStreamMaxLength];
            byte[] btAccumulatedReadBuffer = new byte[JsonParserStream.JsonStreamMaxLength];


            try
            {
                bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 4);

                if (bytesRead == 4)
                {
                    string header = AbstractTransactionFacade.GetString(btPartialReadsBuffer, 4);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, headerArr, 0, 4);


                    if (int.TryParse(header, out bytesToRead) == false)
                    {
                        this._ErrorMessage = String.Format("Error leyendo header. Longitud {0} no valida", header);
                        //  _Log.Error(this._ErrorMessage);
                        this._Status |= TransmissionStatus.HeaderNotFound;
                        return false;
                    }

                }
                else ///Se recibio menos de 6 bytes ....
                {
                    this._ErrorMessage = "Error leyendo datos de conexion: expectedLen no tiene 4 bytes (" + bytesRead.ToString() + ")";
                    // _Log.Error(this._ErrorMessage);
                    this._Status |= TransmissionStatus.HeaderNotFound;
                    return false;
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


            byte[] dump = new byte[bytesToRead + 4 + 1];
            System.Buffer.BlockCopy(headerArr, 0, dump, 0, 4);
            System.Buffer.BlockCopy(btAccumulatedReadBuffer, 0, dump, 4, totalBytesRead);

            _RequestStream.Set(dump, totalBytesRead + 4);
            //_RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead);
            if (this.IsKeepAliveMessage() == false)
            {
              //  _Log.InfoFormat("{0}_IN: {1}", _RootSection, ((JsonParserStream)RequestStream).ToString());
                var stub = JsonConvert.DeserializeObject(((JsonParserStream)RequestStream).ToString().Substring(4));
                _Log.InfoFormat("{0}_IN: {2}{1}", _RootSection, JsonConvert.SerializeObject(stub, Formatting.Indented), Environment.NewLine);
            }

            return true;
        }

        public bool IsKeepAliveMessage()
        {
            if ((this.RequestStream as JsonParserStream).IsSet)
            {
                _IsKeepAliveMessage = (this.RequestStream as JsonParserStream).Get(4) == "0000";

            }
            else
                _IsKeepAliveMessage = false;

            return _IsKeepAliveMessage;
        }
        #endregion

        #region ITransactionParserAssembleable Members
        public bool Assemble()
        {
            bool boolResult = false;

            try
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };

                string output = JsonConvert.SerializeObject(this._ResponseStructure, settings);
                (this._ResponseStream as JsonParserStream).Set(String.Format("{0:D4}{1}", output.Length, output));

                boolResult = true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = String.Format("Exception found at Assemble {0}", ex);
                _Status = TransmissionStatus.BadAssembling;
                boolResult = false;
            }

            return boolResult;
        }

        public bool Disassemble()
        {
            bool boolResult = false;

            try
            {
                var output = (this._RequestStream as JsonParserStream).Get();
                var length = output.Substring(0, 4);
                var data = output.Substring(4);
                //this._RequestStructure = JsonConvert.DeserializeObject(data, this._RequestStructure.GetType()) as JsonParserResponseStructure;
                
                (this._RequestStructure as JsonParserRequestStructure).Root = JsonConvert.DeserializeObject(data);

                boolResult = true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = String.Format("Exception found at Disassemble {0}", ex);
                _Status = TransmissionStatus.BadDisassembling;
                boolResult = false;
            }

            return boolResult;
        }
        #endregion
    }
}
