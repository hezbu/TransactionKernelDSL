using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTcpFunneledOutputTransactionEngine : AbstractFunneledOutputTransactionEngine
    {
        #region Member Fields
        protected IPAddress _IPAddress;
        protected int _TcpPort;
        protected int _Timeout;
        protected new TcpClient _AssignedClient;
        #endregion

        #region Member Properties
        public IPAddress IPAddress
        {
            set { _IPAddress = value; }
        }

        public int TcpPort
        {
            set { _TcpPort = value; }
        }

        public int Timeout
        {
            set 
            { 
                _Timeout = value;
                _ResolveTimeout = value;
            }
        } 
        #endregion

        #region Constructor
        public AbstractTcpFunneledOutputTransactionEngine()
            : base()
        {
            _AssignedClient = new TcpClient();
        }
        #endregion

        #region Abstract Methods
        protected abstract bool UponReceiptProcess(byte[] msg);
        protected abstract bool UponLinkProcess(); 
        #endregion

        protected override bool SetRequirement()
        {
            bool sent = false;
          
            do
            {                
                if (_AssignedParser.SendMethod != null && _AssignedParser.SendMethod(_AssignedClient))
                {                   
                    sent = true;
                    Thread.Sleep(10);
                }
                else if (_AssignedParser.SendMethod == null)
                {
                    _EngineLog.Fatal("Parser's SendMethod delegate is not linked to any valid method, cannot send any messages!");
                    break;
                }
                else
                {
                    _EngineLog.Error("Nexus not ready to send, waiting 2 secs");
                    Thread.Sleep(2000);
                }

            } while (sent == false);

            return sent;
        }
        protected override List<byte[]> GetAvailableResponses()
        {
            List<byte[]> readMessages = new List<byte[]>();

            _AssignedParser.RequestStream.Set(new byte[1024], 1024);
            _AssignedParser.ResetParserStatus();

            #region Reading Flow
            try
            {
                while (_AssignedClient.GetStream().DataAvailable == true)
                {
                    
                    if (_AssignedParser.ReceiveMethod != null && _AssignedParser.ReceiveMethod(_AssignedClient))
                    {
                        UponReceiptProcess(_AssignedParser.RequestStream.Get());
                        
                        readMessages.Add(_AssignedParser.RequestStream.Get());
                    }
                    else if (_AssignedParser.ReceiveMethod == null)
                    {
                        _EngineLog.Fatal("Parser's ReceiveMethod is not linked to any valid method. Cannot receive any messages!");
                    }
                    else
                    {
                        _EngineLog.Error("Error during PullAvailableRequirements(): " + _AssignedParser.ErrorMessage + " (" + (TransmissionStatus)_AssignedParser.Status + ")");
                    }
                }
            }
            #endregion
            #region Reconnect Flow
            catch (InvalidOperationException invOpEx)
            {
                _IsForwarding = false;
                _AssignedClient = new TcpClient();       //Reinstancio el objeto
                _AssignedClient.ReceiveTimeout = _Timeout * 1000;   //Reinstancio el objeto

                _EngineLog.Info("Connecting to " + " " + _IPAddress.ToString() + ":" + _TcpPort + ". " + "(" + invOpEx.Message + ")");

                _AssignedClient.Connect(_IPAddress, _TcpPort);
                _EngineLog.Info("Connected OK to " + _IPAddress.ToString() + ":" + _TcpPort);
                _IsForwarding = true;

                UponLinkProcess();

            }
            #endregion

            return readMessages;
        }
    }
}
