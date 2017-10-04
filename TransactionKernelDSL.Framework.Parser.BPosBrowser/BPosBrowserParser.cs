using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.BPosBrowser
{
    public class BPosBrowserParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public BPosBrowserParser(string rootSection = "BPosBrowser", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStructure = new BPosBrowserStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Request : AbstractTransactionParserStructureType.Response, _RootSection);
            this._ResponseStructure = new BPosBrowserStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Response : AbstractTransactionParserStructureType.Request, _RootSection);

            this._RequestStream = new BPosBrowserStream();
            this._ResponseStream = new BPosBrowserStream();
        }

        #region ITransactionParserCommunicable Members

        public bool Send(object handler)
        {
            try
            {
                string package = (ResponseStream as BPosBrowserStream).Get();
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(AbstractTransactionFacade.GetBytes(package), 0, AbstractTransactionFacade.GetBytes(package).Length);
                    _Log.Info(_RootSection + "_OUT: " + ((BPosBrowserStream)ResponseStream).ToString());
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
            byte[] headerArr = new byte[6 + 1];
            int bytesToRead = BPosBrowserStream.BPosBrowserMaxLength;
            byte[] btPartialReadsBuffer = new byte[BPosBrowserStream.BPosBrowserMaxLength];
            byte[] btAccumulatedReadBuffer = new byte[BPosBrowserStream.BPosBrowserMaxLength];


            try
            {
                bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 6);

                if (bytesRead == 6)
                {
                    string header = AbstractTransactionFacade.GetString(btPartialReadsBuffer, 6);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, headerArr, 0, 6);

                    if (header.Contains("GA") == false)
                    {

                        this._ErrorMessage = "Error leyendo header. GA no encontrado";
                        //   _Log.Error(this._ErrorMessage);
                        this._Status |= TransmissionStatus.HeaderNotFound;
                        return false;
                    }
                    if (int.TryParse(header.Substring(2, 4), out bytesToRead) == false)
                    {
                        this._ErrorMessage = String.Format("Error leyendo header. Longitud {0} no valida", header.Substring(2, 4));
                        //  _Log.Error(this._ErrorMessage);
                        this._Status |= TransmissionStatus.HeaderNotFound;
                        return false;
                    }

                }
                else ///Se recibio menos de 6 bytes ....
                {
                    this._ErrorMessage = "Error leyendo datos de conexion: expectedLen no tiene 6 bytes (" + bytesRead.ToString() + ")";
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


            byte[] dump = new byte[bytesToRead + 6 + 1];
            System.Buffer.BlockCopy(headerArr, 0, dump, 0, 6);
            System.Buffer.BlockCopy(btAccumulatedReadBuffer, 0, dump, 6, totalBytesRead);

            _RequestStream.Set(dump, totalBytesRead + 6);
            //_RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead);
            if(this.IsKeepAliveMessage() == false)
                _Log.Info(_RootSection + "_IN: " + ((BPosBrowserStream)RequestStream).ToString());  

            return true;
        }

        public bool IsKeepAliveMessage()
        {

            if ((this.RequestStream as BPosBrowserStream).IsSet)
            {
                if (this._IsInputParser == true)
                {
                    _IsKeepAliveMessage = (this.RequestStream as BPosBrowserStream).Get(6) == "<T_MSG></T_MSG>";
                }
                else
                {
                    _IsKeepAliveMessage = (this.RequestStream as BPosBrowserStream).Get(6) == "<S_MSG></S_MSG>";
                }
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
                using (var sw = new StringWriter())
                {
                    using (
                        var writer = XmlWriter.Create(sw, new XmlWriterSettings() 
                                     { OmitXmlDeclaration = true})
                    )
                    {
                        if (_IsInputParser == true)
                        {
                            writer.WriteStartElement("S_MSG");
                        }
                        else 
                        {
                            writer.WriteStartElement("T_MSG");
                        }

                        foreach (var item in (this.ResponseStructure as BPosBrowserStructure).Main)
                        {
                            writer.WriteAttributeString(item.Key, item.Value);                           
                        }

                        if ((this.ResponseStructure as BPosBrowserStructure).InputValue.Count > 0)
                        {
                            writer.WriteStartElement("InputValue");
                            foreach (var item in (this.ResponseStructure as BPosBrowserStructure).InputValue)
                            {
                                writer.WriteAttributeString(item.Key, item.Value);
                            }
                            writer.WriteEndElement();
                        }

                        writer.WriteFullEndElement();
                    }
                    var data = String.Format("GA{0:D4}{1}", sw.ToString().Length, sw.ToString());
                    (this._ResponseStream as BPosBrowserStream).Set(data);
                }
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
                (RequestStructure as BPosBrowserStructure).Header = (_RequestStream as BPosBrowserStream).Get(0, 2);
                (RequestStructure as BPosBrowserStructure).Length = (_RequestStream as BPosBrowserStream).Get(2, 4);
                
                string payload = (_RequestStream as BPosBrowserStream).Get(6);
                if (_IsInputParser == true)
                {
                    if (payload.Contains("<T_MSG") == false)
                    {
                        _ErrorMessage = String.Format("<T_MSG not found in stream");
                        _Status |= TransmissionStatus.BadDisassembling;
                        return false;
                    }

                    if (payload.Contains("</T_MSG>") == false && payload.EndsWith("/>") == false)
                    {
                        _ErrorMessage = String.Format("</T_MSG> not found in stream");
                        _Status |= TransmissionStatus.BadDisassembling;
                        return false;
                    }

                    using (XmlReader reader = XmlReader.Create(new StringReader(payload)))
                    {
                        reader.ReadToFollowing("T_MSG");
                        for (int attInd = 0; attInd < reader.AttributeCount; attInd++)
                        {
                            reader.MoveToAttribute(attInd);
                            (RequestStructure as BPosBrowserStructure).Main.Add(reader.Name, reader.Value);
                        }
                        reader.ReadToFollowing("InputValue");
                        for (int attInd = 0; attInd < reader.AttributeCount; attInd++)
                        {
                            reader.MoveToAttribute(attInd);
                            (RequestStructure as BPosBrowserStructure).InputValue.Add(reader.Name, reader.Value);
                        }                        
                    }
                }
                else
                {
                    ///TODO: Falta ver cuando esta conectado a un motor de salida
                    if (payload.Contains("<S_MSG") == false)
                    {
                        _ErrorMessage = String.Format("<S_MSG not found in stream");
                        _Status |= TransmissionStatus.BadDisassembling;
                        return false;
                    }

                    if (payload.Contains("</S_MSG>") == false && payload.EndsWith("/>") == false)
                    {
                        _ErrorMessage = String.Format("</S_MSG> not found in stream");
                        _Status |= TransmissionStatus.BadDisassembling;
                        return false;
                    }

                    using (XmlReader reader = XmlReader.Create(new StringReader(payload)))
                    {
                        reader.ReadToFollowing("S_MSG");
                        for (int attInd = 0; attInd < reader.AttributeCount; attInd++)
                        {
                            reader.MoveToAttribute(attInd);
                            (RequestStructure as BPosBrowserStructure).Main.Add(reader.Name, reader.Value);
                        }
                        reader.ReadToFollowing("InputValue");
                        for (int attInd = 0; attInd < reader.AttributeCount; attInd++)
                        {
                            reader.MoveToAttribute(attInd);
                            (RequestStructure as BPosBrowserStructure).InputValue.Add(reader.Name, reader.Value);
                        }
                    }
                }

                boolResult = true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = String.Format("Exception found at Disassemble {0}", ex);
                _Status |= TransmissionStatus.BadDisassembling;
                boolResult = false;
            }

            return boolResult;
        }

        #endregion
    }
}
