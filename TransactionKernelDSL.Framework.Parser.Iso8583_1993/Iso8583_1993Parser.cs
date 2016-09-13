using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Parser.Iso8583_1993
{
    public sealed class Iso8583_1993Parser : AbstractTransactionParser, ITransactionParserCommunicable, ITransactionParserAssembleable
    {
        private bool _IsBitmap128 = false;

        public Iso8583_1993Parser(string rootSection = "Iso8583_1993", bool isListenerParser = true) :
            base(rootSection, isListenerParser)
        {
            this._AssembleMethod = Assemble;
            this._DisassembleMethod = Disassemble;

            this._SendMethod = Send;
            this._ReceiveMethod = Receive;
            this._IsKeepAliveMessageMethod = IsKeepAliveMessage;

            this._RequestStructure = new Iso8583_1993Structure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Request : AbstractTransactionParserStructureType.Response, _RootSection);
            this._ResponseStructure = new Iso8583_1993Structure((_IsInputParser == true) ? AbstractTransactionParserStructureType.Response : AbstractTransactionParserStructureType.Request, _RootSection);
            this._RequestStream = new Iso8583_1993Stream();
            this._ResponseStream = new Iso8583_1993Stream();
        }

        #region ITransactionParserCommunicable Members

        public bool Send(object handler)
        {
            if (((TcpClient)handler).GetStream().CanWrite == true)
            {
                if (this.IsKeepAliveMessage() == false)
                {
                    ((TcpClient)handler).GetStream().Write(ResponseStream.Get(), 0, ResponseStream.Get().Length);
                    _Log.Info(_RootSection + "_OUT: " + ResponseStream.ToString());
                }
                else ((TcpClient)handler).GetStream().Write(new byte[] { 0x06 }, 0, 1);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Receive(object handler)
        {
            int bytesRead = 0;
            byte[] header = null;
            int totalBytesRead = 0;//nHeaderLenProt;

            int bytesToRead = Iso8583_1993Stream.Iso8583_1993MaxLength;
            byte[] btPartialReadsBuffer = new byte[Iso8583_1993Stream.Iso8583_1993MaxLength];
            byte[] btAccumulatedReadBuffer = new byte[Iso8583_1993Stream.Iso8583_1993MaxLength];


            try
            {
                bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, 2);

                if (bytesRead == 2)
                {
                    bytesToRead = (int)btPartialReadsBuffer[0] * 0x100;
                    bytesToRead += (int)btPartialReadsBuffer[1];
                    header = new byte[] { btPartialReadsBuffer[0], btPartialReadsBuffer[1] };
                    this._IsKeepAliveMessage = (header[0] == 0x00 && header[1] == 0x00);
                }
                else ///Se recibio menos de 2 bytes ....
                {
                    this._ErrorMessage = "Error leyendo datos de conexion: expectedLen no tiene 2 bytes (" + bytesRead.ToString() + ")";
                    this._Status |= TransmissionStatus.HeaderNotFound;
                    return false;
                }


                for (totalBytesRead = 0; totalBytesRead < bytesToRead; )
                {
                    bytesRead = ((TcpClient)handler).GetStream().Read(btPartialReadsBuffer, 0, bytesToRead - totalBytesRead);
                    System.Buffer.BlockCopy(btPartialReadsBuffer, 0, btAccumulatedReadBuffer, totalBytesRead, bytesRead);
                    totalBytesRead += bytesRead;

                    if (bytesRead == 0) ///No se leyo nada
                    {
                        break;
                    }
                }


            }
            catch (InvalidOperationException invOpEx)
            {
                this._ErrorMessage = "ERROR: Operación invalida. Mensaje: " + invOpEx.Message;
                this._Status |= TransmissionStatus.SocketError;
                return false;
            }
            catch (Exception ex)
            {
                this._ErrorMessage = "ERROR: TIME OUT antes de determinar el proceso (handler) que requiere la conexión. Mensaje: " + ex.Message;
                this._Status |= TransmissionStatus.Timeout;
                return false;
            }

            byte[] dump = new byte[bytesToRead + 2];
            System.Buffer.BlockCopy(header, 0, dump, 0, 2);
            System.Buffer.BlockCopy(btAccumulatedReadBuffer, 0, dump, 2, totalBytesRead);
            this._RequestStream.Set(dump);
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
            int intLength = 2;
            byte mask = 0x01;
            this._ResponseStream = new Iso8583_1993Stream();
            int intBmpIndex = 0;
            int intFieldFoundIndex = -1;

            byte[] partialData = this._ResponseStream.Get();

            ///// OPTIONAL DATA PREFIX
            if (((Iso8583_1993Structure)ResponseStructure).IsDataPrefix)
            {
                intLength += _ResponseStructure["DataPrefix"].CopyContentTo(ref partialData, intLength);
            }

            ///// OPTIONAL IMS_CICS TRANSACTION CODE
            if (((Iso8583_1993Structure)ResponseStructure).IsImsCicsTransactionCode)
            {
                intLength += _ResponseStructure["IMS_CICS_TransactionCode"].CopyContentTo(ref partialData, intLength);
            }

            //// ISO LITERAL
            intLength += ResponseStructure["StartSignature"].CopyContentTo(ref partialData, intLength);
            //// BASE 24 HEADER
            intLength += ResponseStructure["Base24Header"].CopyContentTo(ref partialData, intLength);
            //// MESSAGE TYPE
            intLength += ResponseStructure["MessageType"].CopyContentTo(ref partialData, intLength);
            //// BITMAP
            intLength += ResponseStructure["PrimaryBitmap"].CopyContentTo(ref partialData, intLength);

            #region Análisis de Bitmaps
            if ((((Iso8583_1993Structure)ResponseStructure).GetBitmapByByte(0) & 0x80) == 0x80)
                this._IsBitmap128 = true;
            else
                this._IsBitmap128 = false;
            #endregion
            #region Análisis de CAMPOS Nuevo
            for (intBmpIndex = 0; intBmpIndex < ((this._IsBitmap128 == true) ? 16 : 8); intBmpIndex++)
            {
                for (int bitIndex = 7; bitIndex >= 0; bitIndex--)
                {
                    if (((int)((Iso8583_1993Structure)ResponseStructure).GetBitmapByByte(intBmpIndex) & (mask << bitIndex)) == (mask << bitIndex))
                    {
                        intFieldFoundIndex = (intBmpIndex * 8) + (8 - bitIndex);
                        intLength += ResponseStructure[intFieldFoundIndex.ToString()].CopyContentTo(ref partialData, intLength);
                        Debug.WriteLine("[Assemble:Field " + intFieldFoundIndex.ToString().PadLeft(2, '0') + "]" + ResponseStructure[intFieldFoundIndex.ToString()].ToString());
                    }
                }
            }
            #endregion

            if (((Iso8583_1993Structure)ResponseStructure).IsIso8583RequestMsgType())
            {
                System.Buffer.BlockCopy(new byte[] { (byte)'?' }, 0, partialData, intLength, 1);
                intLength += 1;
            }

            #region Carga de campo LONGITUD al principio de la trama
            byte[] length = new byte[2 + 1];
            AbstractTransactionFacade.AscToBcd(length, AbstractTransactionFacade.GetBytes((intLength - 2).ToString("X").PadLeft(4, '0')), 4, 0);
            ResponseStructure["LENGTH"].CopyContentFrom(length);
            ResponseStructure["LENGTH"].CopyContentTo(ref partialData);
            Debug.WriteLine("Length Header = " + AbstractTransactionFacade.GetString(length, 2));
            #endregion

            this._ResponseStream.Set(partialData, intLength);

            Debug.WriteLine("[Assemble 24] Longitud trama: " + intLength.ToString());
            Debug.WriteLine("[Assemble 24] Trama: " + this._ResponseStream.ToString());

           

            boolResult = true;

            return boolResult;
        }

        public bool Disassemble()
        {
            bool boolResult = false;
            int intFieldFoundIndex = -1;

            byte mask = 0x01;

            try
            {
                #region Finding ISO start of frame
                int intLength = ((Iso8583_1993Structure)_RequestStructure).FindISOStart(RequestStream.Get());
                if (intLength == 11)
                {
                    RequestStructure["DataPrefix"].CopyContentFrom(RequestStream.Get(), 0);
                }
                else if (intLength == 20)
                {
                    RequestStructure["DataPrefix"].CopyContentFrom(RequestStream.Get(), 0);
                    RequestStructure["IMS_CICS_TransactionCode"].CopyContentFrom(RequestStream.Get(), 9);
                }
                else if (intLength != 2)
                {
                    throw new Exception("Signature 'ISO' haven't found on where it was expected to be, but in position " + intLength.ToString());
                }

                intLength += 3; //Salteo el ISO Literal...

                #endregion

                intLength += RequestStructure["Base24Header"].CopyContentFrom(_RequestStream.Get(), intLength);
                Debug.WriteLine("[Dissasemble B24]: " + RequestStructure["Base24Header"].Keyname + ": " + RequestStructure["Base24Header"].ToString());
                intLength += RequestStructure["MessageType"].CopyContentFrom(_RequestStream.Get(), intLength);
                Debug.WriteLine("[Dissasemble B24]: " + RequestStructure["MessageType"].Keyname + ": " + RequestStructure["MessageType"].ToString());
                intLength += RequestStructure["PrimaryBitmap"].CopyContentFrom(_RequestStream.Get(), intLength);
                Debug.WriteLine("[Dissasemble B24]: " + RequestStructure["PrimaryBitmap"].Keyname + ": " + RequestStructure["PrimaryBitmap"].ToString());

                #region Bitmaps Analysis
                if ((((Iso8583_1993Structure)RequestStructure).GetBitmapByByte(0) & 0x80) == 0x80)
                {
                    this._IsBitmap128 = true;
                }
                else
                {
                    this._IsBitmap128 = false;
                }
                #endregion

                #region Analisis de CAMPOS (nuevo)
                for (int intBmpIndex = 0; intBmpIndex < ((this._IsBitmap128 == true) ? 16 : 8); intBmpIndex++)
                {
                    for (int bitIndex = 7; bitIndex >= 0; bitIndex--)
                    {
                        if (((int)((Iso8583_1993Structure)RequestStructure).GetBitmapByByte(intBmpIndex) & (mask << bitIndex)) == (mask << bitIndex))
                        {
                            intFieldFoundIndex = (intBmpIndex * 8) + (8 - bitIndex);
                            intLength += RequestStructure[intFieldFoundIndex.ToString()].CopyContentFrom(RequestStream.Get(), intLength);
                            Debug.WriteLine("[Disassemble:Field " + intFieldFoundIndex.ToString().PadLeft(2, '0') + "]" + RequestStructure[intFieldFoundIndex.ToString()].ToString());

                        }
                    }
                }
                #endregion

                boolResult = true;
            }
            catch (Exception ex)
            {
                this._ErrorMessage = "Excepcion procesando bits " + intFieldFoundIndex.ToString() + " - Mensaje= " + ex.Message;
                this._Status |= TransmissionStatus.BadDisassembling;

                (ResponseStructure["StartSignature"] as Iso8583_1993Field).CopyContentFrom("ISO");
                (ResponseStructure["Base24Header"] as Iso8583_1993Field).CopyContentFrom((RequestStructure["Base24Header"] as Iso8583_1993Field).ToString().Substring(0, 4) + intFieldFoundIndex.ToString().PadLeft(3, '0') + (RequestStructure["Base24Header"] as Iso8583_1993Field).ToString().Substring(7));
                (ResponseStructure["MessageType"] as Iso8583_1993Field).CopyContentFrom("9" + (RequestStructure["MessageType"] as Iso8583_1993Field).ToString().Substring(1));
                (ResponseStructure["PrimaryBitmap"] as Iso8583_1993Field).CopyContentFrom("0000000000000000");
                
                if (this.Assemble() == true)
                {
                    this._ErrorMessage += " Se genero trama 9XXX de error";
                }

                boolResult = false;
            }


            return boolResult;
        }

        #endregion
    }
}
