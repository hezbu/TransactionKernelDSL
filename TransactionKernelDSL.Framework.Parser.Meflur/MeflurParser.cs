using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;
using System.Net.Sockets;
using System.Diagnostics;
using System.Configuration;

namespace TransactionKernelDSL.Framework.Parser.Meflur
{
    public class MeflurParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public MeflurParser(string rootSection = "Meflur", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;
           

            // Debug.WriteLine("Request Structure is:");
            this._RequestStructure = new MeflurStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Request : AbstractTransactionParserStructureType.Response, _RootSection);
            // Debug.WriteLine("Response Structure is:");
            this._ResponseStructure = new MeflurStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Response : AbstractTransactionParserStructureType.Request, _RootSection);

            this._RequestStream = new MeflurStream();
            this._ResponseStream = new MeflurStream();
        }

        #region ITransactionParserCommunicable Members

        public bool Send(object handler)
        {
            try
            {
                if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, (ResponseStream as MeflurStream).Length);
                    _Log.Info(_RootSection + "_OUT: " + ResponseStream.ToString());
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
            bool boolEtxFound = false;
            byte[] accumulatedBytesRead = new byte[8192];

            DateTime dtStart = System.DateTime.Now;
            bytesRead = 0;

            do
            {
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
                    else
                    {
                        for (int i = 0; i < totalBytesRead; i++)
                        {
                            if (accumulatedBytesRead[i] == 0x03)
                            {
                                totalBytesRead = i + 1;
                                boolEtxFound = true;
                                break;
                            }
                        }
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

            }
            while (boolEtxFound == false);

            this.RequestStream.Set(accumulatedBytesRead, totalBytesRead);
            _Log.Info(_RootSection + "_IN: " + RequestStream.ToString());
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
            int intLength = 1;
            this._ResponseStream = new MeflurStream();
            int dynamicFieldsAssembled = 0;

            byte[] partialData = this._ResponseStream.Get();

            try
            {
                ResponseStream[0] = MeflurStream.STX;

                foreach (MeflurField f in ResponseStructure.Fields.Values)
                {
                    if (f.IsRequired == true)
                    {
                        if (String.IsNullOrEmpty(f.Content)) throw new ApplicationException("Meflur's mandatory field " + f.Keyname + " hasn't been initialized!");
                        intLength += f.CopyContentTo(ref partialData, intLength);
                    }
                    else if (f.HasContent == true)
                    {
                        intLength += f.CopyContentTo(ref partialData, intLength);
                        dynamicFieldsAssembled++;
                        if (dynamicFieldsAssembled < ((MeflurStructure)ResponseStructure).ActiveDynamicFields)
                        {
                            ResponseStream[intLength] = MeflurStream.EOF;
                            intLength++;
                        }
                        else
                        {
                            ResponseStream[intLength] = MeflurStream.ETX;
                            intLength++;
                            break;
                        }


                    }
                }
                              

                this._ResponseStream.Set(partialData, intLength);
                boolResult = true;

            }
            catch (Exception ex)
            {
                _ErrorMessage = "Error ensamblando: " + ex.Message;
                _Status = TransmissionStatus.BadAssembling;
                boolResult = false;
            }

            return boolResult;
        }

        public bool Disassemble()
        {
            try
            {
                int processingOffset = 1;
                #region Checking STX and ETX...
                if ((RequestStream as MeflurStream).Validate() == false)
                {
                    throw new ApplicationException("STX and/or ETX not found");
                }               
                #endregion
                #region Disassembling stage
                foreach (MeflurField f in RequestStructure.Fields.Values)
                {
                    if (f.IsRequired == true)
                    {
                        processingOffset += f.CopyContentFrom(_RequestStream.Get(), processingOffset);
                        Debug.WriteLine("[Disassemble:MeflurParser]:" + f.Keyname + " = " + f.Content);
                    }
                    else break;
                }

                string strStream = AbstractTransactionFacade.GetString(RequestStream.Get(), (RequestStream as MeflurStream).Length - processingOffset - 1, processingOffset);
                Debug.WriteLine("[Disassemble:MeflurParser]: Raw Stream = " + strStream);



                string[] strArrFields = strStream.Split(new char[] { (char)MeflurStream.EOF });
                for (int i = 0; i < strArrFields.Length; i++)
                {
                    ((MeflurField)RequestStructure[i.ToString()]).Content = strArrFields[i];
                    Debug.WriteLine("[Disassemble:MeflurParser]: Field = " + strArrFields[i]);

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

                        FieldsCollection fc = null;
                        if (RequestStructure.StructureType == AbstractTransactionParserStructureType.Request)
                            fc = t.RequirementFields;
                        else fc = t.ResponseFields;

                        #region Análisis de campos
                        foreach (FieldElement f in fc)
                        {
                            if (RequestStructure.Fields.ContainsKey(f.Id) == true)
                            {
                                #region Analisis de Subcampos
                                foreach (SubFieldElement sf in f.Subfields)
                                {
                                    MeflurSubfield newSubfield = new MeflurSubfield()
                                    {
                                        Description = sf.Description,
                                        Id = sf.Id,
                                        Keyname = sf.Keyname,
                                        Length = sf.Length,
                                        Type = (AbstractTransactionParserFieldType)Enum.Parse(typeof(AbstractTransactionParserFieldType), sf.Type),
                                        Offset = sf.Offset
                                    };

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
                                return false;
                            }
                        }
                        break;
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                _ErrorMessage = "Error desensamblando: " + ex.Message;
                _Status |= TransmissionStatus.BadDisassembling;
                return false;
            }

            return true;
        }

        #endregion
    }
}
