using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTcpStraightOutputTransactionEngine : AbstractStraightOutputTransactionEngine
    {
        protected IPAddress _IPAddress;
        protected int _TcpPort;
        protected int _Timeout;
        protected new TcpClient _AssignedClient = null;

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
            }
        }
        #endregion

        protected override IDisposable OutputHandlerFactory(object state = null)
        {
            return new TcpClient();
        }

        protected override bool Connect(object handler)
        {
            try
            {
                (handler as TcpClient).ReceiveTimeout = _Timeout * 1000;
                (handler as TcpClient).Connect(_IPAddress, _TcpPort);
                _EngineLog.Info("Connected OK to " + _IPAddress.ToString() + ":" + _TcpPort);
                return true;
            }
            catch (Exception ex)
            {
                _EngineLog.Error("Exception found while connecting to " + _IPAddress.ToString() + ":" + _TcpPort + ": " + ex.Message);
                _EngineLog.Error("SS: " + ex.StackTrace);
                return false;
            }
        }

        protected override bool Disconnect(object handler)
        {
            try
            {
                (handler as TcpClient).Client.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                _EngineLog.Error("Exception found while disconnecting from " + _IPAddress.ToString() + ":" + _TcpPort + ": " + ex.Message);
                _EngineLog.Error("SS: " + ex.StackTrace);
                return false;
            }
        }
    }
}
