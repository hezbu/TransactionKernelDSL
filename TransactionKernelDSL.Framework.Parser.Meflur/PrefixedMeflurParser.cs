using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using TransactionKernelDSL.Framework.V1;
using TransactionKernelDSL.Framework.Parser.Meflur;

namespace TransactionKernelDSL.Framework.Parser.PrefixedMeflur
{
    public class PrefixedMeflurParser : MeflurParser
    {
        public PrefixedMeflurParser(string rootSection = "Meflur", bool isEngineParser = true)
            : base(rootSection, isEngineParser)
        {
            this._SendMethod = PrefixedSend;
            this._ReceiveMethod = PrefixedReceive;
        }

        public bool PrefixedSend(object handler)
        {
            try
            {
                if (IsKeepAliveMessage() == false)
                {
                    byte[] length = new byte[2 + 1];
                    AbstractTransactionFacade.AscToBcd(length, AbstractTransactionFacade.GetBytes((ResponseStream as MeflurStream).Length.ToString("X").PadLeft(4, '0')), 4, 0);
             
                    ((TcpClient)handler).GetStream().Write(length, 0, 2);
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

        public bool PrefixedReceive(object handler)
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
                    bytesRead = ((TcpClient)handler).GetStream().Read(accumulatedBytesRead, 0, 2);
                   
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


    }
}
