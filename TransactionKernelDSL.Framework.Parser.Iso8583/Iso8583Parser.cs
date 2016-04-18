using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;
using System.Diagnostics;
using System.Configuration;
using System.Net.Sockets;

namespace TransactionKernelDSL.Framework.Parser.Iso8583
{
    public sealed class Iso8583Parser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public Iso8583Parser(string rootSection = "Iso8583", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;
                        
            this._RequestStructure = new Iso8583Structure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Request : AbstractTransactionParserStructureType.Response, _RootSection);
            this._ResponseStructure = new Iso8583Structure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Response : AbstractTransactionParserStructureType.Request, _RootSection);
         
            this._RequestStream = new Iso8583Stream();
            this._ResponseStream = new Iso8583Stream();
        }

        #region ITransactionParserCommunicable Members

        public bool Send(object handler)
        {
            try
            {
                if (this.Status == TransmissionStatus.BadDisassembling) return true;
                else if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, ResponseStream.Get().Length);
                    _Log.Info(_RootSection + "_OUT: " + ((Iso8583Stream)ResponseStream).ToString());
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
            int bytesRead = 0;
            int totalBytesRead = 0;
            byte[] Header = null;
            int bytesToRead = Iso8583Stream.Iso8583MaxLength;
            byte[] btPartialReadsBuffer = new byte[Iso8583Stream.Iso8583MaxLength];
            byte[] btAccumulatedReadBuffer = new byte[Iso8583Stream.Iso8583MaxLength];

           
            try
            {
                bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 2);

                if (bytesRead == 2)
                {
                    bytesToRead = (int)btPartialReadsBuffer[0] * 0x100;
                    bytesToRead += (int)btPartialReadsBuffer[1];
                    Header = new byte[] { btPartialReadsBuffer[0], btPartialReadsBuffer[1] };
                    this._IsKeepAliveMessage = (Header[0] == 0x00 && Header[1] == 0x00);
                  
                }
                else ///Se recibio menos de 2 bytes ....
                {
                    this._ErrorMessage = "Error leyendo datos de conexion: expectedLen no tiene 2 bytes (" + bytesRead.ToString() + ")";
                    this._Status |= TransmissionStatus.HeaderNotFound;
                    return false;
                }

                //blocks until a client sends a message
                for (totalBytesRead = 0; (totalBytesRead < bytesToRead) && (this._IsKeepAliveMessage == false); )
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, bytesToRead - totalBytesRead);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;

                    if (bytesRead == 0) ///No se leyo nada
                    {
                        this._ErrorMessage = "Error leyendo datos de conexion: bytesRead = 0 ";
                        this._Status |= TransmissionStatus.ContactLost;
                        return false;
                    }
                }

                //break;
            }
            catch (Exception ex)
            {
                this._ErrorMessage = "TIMEOUT antes de determinar el proceso (handler) que requiere la conexión. Mensaje: " + ex.Message;
                this._Status |= TransmissionStatus.Timeout;
                return false;
            }

            
            byte[] dump = new byte[bytesToRead + 2 + 1];
            System.Buffer.BlockCopy(Header, 0, dump, 0, 2);
            System.Buffer.BlockCopy(btAccumulatedReadBuffer, 0, dump, 2, totalBytesRead);

            _RequestStream.Set(dump, totalBytesRead + 2);
            //_RequestStream.Set(btAccumulatedReadBuffer, totalBytesRead);
            _Log.Info(_RootSection + "_IN: " + ((Iso8583Stream)RequestStream).ToString());
            return true;
        }

        public bool IsKeepAliveMessage()
        {
            return _IsKeepAliveMessage;
        }

        #endregion

        #region ITransactionParserAssembleable Members

        public bool Assemble()
        {
            bool boolResult = false;
            int intLength = 2;
            this._ResponseStream = new Iso8583Stream();
            int intFieldFoundIndex = -1;
            byte mask = 0x01;

            byte[] partialData = this._ResponseStream.Get();

            try
            {
                intLength += _ResponseStructure["TPDU"].CopyContentTo(ref partialData, intLength);
                intLength += _ResponseStructure["MSGID"].CopyContentTo(ref partialData, intLength);
                intLength += _ResponseStructure["BITMAP"].CopyContentTo(ref partialData, intLength);

                #region Carga de campos
                
                for (int intBmpIndex = 0; intBmpIndex < 8; intBmpIndex++)
                {
                    for (int bitIndex = 7; bitIndex >= 0; bitIndex--)
                    {
                        if ((ResponseStructure["BITMAP"].Content[intBmpIndex] & (mask << bitIndex)) == (mask << bitIndex)) //BIT 7
                        {
                            intFieldFoundIndex = (intBmpIndex * 8) + (8 - bitIndex);
                            intLength += _ResponseStructure[intFieldFoundIndex.ToString()].CopyContentTo(ref partialData, intLength);
                            Debug.WriteLine("[Assemble:Field " + intFieldFoundIndex.ToString().PadLeft(2, '0') + "]" + _ResponseStructure[intFieldFoundIndex.ToString()].ToString());
                        }
                    }
                }
                #endregion
                #region Carga de campo LONGITUD al principio de la trama
                byte[] length = new byte[2 + 1];
                AbstractTransactionFacade.AscToBcd(length, AbstractTransactionFacade.GetBytes((intLength - 2).ToString("X").PadLeft(4, '0')), 4, 0);
                _ResponseStructure["LENGTH"].CopyContentFrom(length);
                _ResponseStructure["LENGTH"].CopyContentTo(ref partialData);
                #endregion

                this._ResponseStream.Set(partialData, intLength);


                boolResult = true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Excepcion procesando bits " + intFieldFoundIndex.ToString() + " - Mensaje= " + ex.Message;
                _Status = TransmissionStatus.BadAssembling;
                boolResult = false;
            }

            return boolResult;
        }

        public bool Disassemble()
        {
            bool boolResult = false;
            int intFieldFoundIndex = -1;
            byte mask = 0x01;
            try
            {

                int intLength = 0;
                intLength += _RequestStructure["LENGTH"].CopyContentFrom(_RequestStream.Get(), intLength);
                intLength += _RequestStructure["TPDU"].CopyContentFrom(_RequestStream.Get(), intLength);
                intLength += _RequestStructure["MSGID"].CopyContentFrom(_RequestStream.Get(), intLength);
                intLength += _RequestStructure["BITMAP"].CopyContentFrom(_RequestStream.Get(), intLength);


                #region Análisis de campos por defecto del protocolo
                ///ANALISIS DE FIELDS
                for (int intBmpIndex = 0; intBmpIndex < 8; intBmpIndex++)
                {
                    for (int bitIndex = 7; bitIndex >= 0; bitIndex--)
                    {
                        if ((RequestStructure["BITMAP"].Content[intBmpIndex] & (mask << bitIndex)) == (mask << bitIndex))
                        {
                            intFieldFoundIndex = (intBmpIndex * 8) + (8 - bitIndex);
                            intLength += _RequestStructure[intFieldFoundIndex.ToString()].CopyContentFrom(_RequestStream.Get(), intLength);
                            Debug.WriteLine("[Disassemble:Field " + intFieldFoundIndex.ToString().PadLeft(2, '0') + "]" + _RequestStructure[intFieldFoundIndex.ToString()].ToString());
                        }
                    }
                }
                #endregion
                #region Análisis de campos particular a la transaccion
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AbstractTransactionParserSection defaultLayout = config.GetSection(_RootSection) as AbstractTransactionParserSection;

                if (defaultLayout == null) throw new ApplicationException("ConfigSection " + _RootSection + " not found on app.config!");

                foreach (TransactionElement t in defaultLayout.Transactions)
                {
                    #region Buscando transacción
                    if (t.Id == RequestStructure.GetOperationId().ToString())
                    {
                        #region Análisis de campos
                        foreach (FieldElement f in t.RequirementFields)
                        {
                            if (_RequestStructure.Fields.ContainsKey(f.Id) == true)
                            {
                                #region Analisis de Subcampos
                                foreach (SubFieldElement sf in f.Subfields)
                                {
                                    Iso8583Subfield newSubfield = new Iso8583Subfield()
                                    {
                                        Description = sf.Description,
                                        Id = sf.Id,
                                        Keyname = sf.Keyname,
                                        Length = sf.Length,
                                        Type = (AbstractTransactionParserFieldType)Enum.Parse(typeof(AbstractTransactionParserFieldType), sf.Type),
                                        Offset = sf.Offset
                                    };

                                    newSubfield.CreateContent();
                                    newSubfield.CopyContentFrom(RequestStructure[f.Id].Content);
                                    RequestStructure[f.Id].Subfields.Add(newSubfield.Id, newSubfield);
                                }
                                #endregion
                                RequestStructure[f.Id].Alias = f.Keyname;
                            }
                            else
                            {
                                _ErrorMessage = "Error processing .config default request structure versus " + t.Name + " request structure. Field " + f.Id + "-" + f.Keyname + " doesn't exist on default!";
                                _Status |= TransmissionStatus.BadDisassembling;
                                boolResult = false;
                            }
                        }
                        break;
                        #endregion
                    }
                    #endregion
                }
                #endregion
                boolResult = true;
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Excepcion procesando bits " + intFieldFoundIndex.ToString() + " - Mensaje= " + ex.Message;
                _Status |= TransmissionStatus.BadDisassembling;
                boolResult = false;
            }

            return boolResult;
        }

        #endregion              
    }
}
