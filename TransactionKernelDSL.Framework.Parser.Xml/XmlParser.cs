using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Xml
{
    public class XmlParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public XmlParser(string rootSection = "Xml", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            if (_IsInputParser == true)
            {
                this._RequestStructure = new XmlRequestStructure();
                this._ResponseStructure = new XmlResponseStructure();
            }
            else
            {
                this._RequestStructure = new XmlResponseStructure();
                this._ResponseStructure = new XmlRequestStructure();
            }

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
            int bytesToRead = XmlStream.XmlStreamMaxLength;
            byte[] btPartialReadsBuffer = new byte[XmlStream.XmlStreamMaxLength];
            byte[] btAccumulatedReadBuffer = new byte[XmlStream.XmlStreamMaxLength];


            try
            {

                //blocks until a client sends a message
                for (totalBytesRead = 0; (totalBytesRead < bytesToRead); )
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, bytesToRead - totalBytesRead);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;

                    var endTest = AbstractTransactionFacade.GetString(btAccumulatedReadBuffer, totalBytesRead);
                    if (_IsInputParser == true)
                    {
                        if (endTest.Contains("</T_MSG>")) break;
                    }
                    else
                    {
                        if (endTest.Contains("</S_MSG>")) break;
                    }

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

            _RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead );

            if (this.IsKeepAliveMessage() == false)
                _Log.Info(_RootSection + "_IN: " + ((XmlStream)RequestStream).ToString());

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
            bool boolResult = false;

            try
            {
                XmlDocument doc = new XmlDocument();
                var rsp = this._ResponseStructure as XmlResponseStructure;

                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", null, null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element1 = null;
                if (this._IsInputParser == true)
                {
                    element1 = doc.CreateElement(string.Empty, "S_MSG", string.Empty);
                    doc.AppendChild(element1);
                    /*
                     * MTI=\"3210\" PCODE=\"000000\" CID=\"00000000\" TID=\"4D790756\" LTE=\"1\" TRC=\"38\" COP=\"001\" DTMS=\"20160623T205603\
                     */

                    element1.SetAttribute("MTI", rsp.MTI);
                    element1.SetAttribute("PCODE", rsp.PCODE);
                    element1.SetAttribute("CID", rsp.CID);
                    element1.SetAttribute("TID", rsp.TID);
                    element1.SetAttribute("LTE", rsp.LTE);
                    element1.SetAttribute("TRC", rsp.TRC);
                    element1.SetAttribute("COP", rsp.COP);
                    element1.SetAttribute("DTMS", rsp.DTMS);
                }
                else
                {
                    element1 = doc.CreateElement(string.Empty, "T_MSG", string.Empty);
                    doc.AppendChild(element1);

                    element1.SetAttribute("MTI", rsp.MTI);
                    element1.SetAttribute("PCODE", rsp.PCODE);
                    element1.SetAttribute("TID", rsp.TID);
                    element1.SetAttribute("CID", rsp.CID);
                    element1.SetAttribute("NID", rsp.NID);
                    element1.SetAttribute("VAPP", rsp.VAPP);
                    element1.SetAttribute("TRC", rsp.TRC);
                    element1.SetAttribute("LTE", rsp.LTE);
                    element1.SetAttribute("COP", rsp.COP);
                    element1.SetAttribute("DTMP", rsp.DTMP);
                }



                if (rsp.Response != null)
                {
                    element1.InnerXml = rsp.Response.Serialize();
                }

                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    doc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    (this._ResponseStream as XmlStream).Set(String.Format("{0}", stringWriter.GetStringBuilder().ToString()));

                }




                //XmlElement element3 = doc.CreateElement(string.Empty, "level2", string.Empty);
                //XmlText text1 = doc.CreateTextNode("text");
                //element3.AppendChild(text1);
                //element2.AppendChild(element3);

                //XmlElement element4 = doc.CreateElement(string.Empty, "level2", string.Empty);
                //XmlText text2 = doc.CreateTextNode("other text");
                //element4.AppendChild(text2);
                //element2.AppendChild(element4);

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
                var output = (this._RequestStream as XmlStream).Get();

                using (StringReader strReader = new StringReader(output))
                {
                    var xDoc = XDocument.Load(strReader);
                    (this._RequestStructure as XmlRequestStructure).Root = XmlDynamicHelper.Parse((this._RequestStructure as XmlRequestStructure).Root, xDoc.Elements().First());
                }

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
