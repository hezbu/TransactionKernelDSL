using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Beltran
{
    public class BeltranParser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        public BeltranParser(string rootSection = "Beltran", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;


            // Debug.WriteLine("Request Structure is:");
            this._RequestStructure = new BeltranStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Request : AbstractTransactionParserStructureType.Response, _RootSection);
            // Debug.WriteLine("Response Structure is:");
            this._ResponseStructure = new BeltranStructure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Response : AbstractTransactionParserStructureType.Request, _RootSection);

            this._RequestStream = new BeltranStream();
            this._ResponseStream = new BeltranStream();
        }

        public bool Send(object handler)
        {
            try
            {
                if (IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, (ResponseStream as BeltranStream).Length);
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

        public bool Assemble()
        {
            bool boolResult = false;
            int intLength = 0;
            this._ResponseStream = new BeltranStream();
            int dynamicFieldsAssembled = 0;
            byte[] partialData = this._ResponseStream.Get();

            try
            {
                foreach (BeltranField f in ResponseStructure.Fields.Values)
                {
                    if (f.HasContent == true)
                    {
                        intLength += f.CopyContentTo(ref partialData, intLength);
                        ResponseStream[intLength] = BeltranStream.PIPE;
                        intLength++;
                        dynamicFieldsAssembled++;
                    }
                }
                (this._ResponseStream as BeltranStream).Set(partialData, intLength - 1);
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
                int processingOffset = 0;
                int index = 0;
                #region Disassemble start
                //Debug.WriteLine("[Disassemble:BeltranParser]: Raw Stream = " + this._RequestStream.ToString());
                string[] strBeltranFields = this._RequestStream.ToString().Split('|');

                foreach (BeltranField f in RequestStructure.Fields.Values)
                {
                    if (processingOffset >= _RequestStream.Get().Length) break;

                    processingOffset += f.CopyContentFrom(_RequestStream.Get(), processingOffset, strBeltranFields[index].Length);
                    processingOffset++;
                    //Debug.WriteLine("[Disassemble:BeltranParser]:" + f.Keyname + " = " + f.Content + " length=" + _RequestStream.Get().Length.ToString() + " offset:" + processingOffset.ToString());
                    index++;
                }

                //for (int i = 0; i < strBeltranFields.Length; i++)
                //{

                //    ((BeltranField)RequestStructure[i.ToString()]).Content = strBeltranFields[i];
                //    Debug.WriteLine("[Disassemble:BeltranParser]: Field = " + strBeltranFields[i]);
                //}
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
    }
}
