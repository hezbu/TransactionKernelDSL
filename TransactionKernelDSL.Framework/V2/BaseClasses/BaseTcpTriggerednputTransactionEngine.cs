using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TransactionKernelDSL.Framework.V2.BaseClasses
{
    public abstract class BaseTcpTriggerednputTransactionEngine 
        : BaseTransactionEngine, ITcpTriggeredTransactionInputEngine
    {
        private TcpListener _Listener = null;
        private int _ListenerTimeout = 60;
        private IPAddress _ListenerIpAddress = IPAddress.Parse("127.0.0.1");
        private int _ListenerTcpPort = 8888;
        private int _ListenerMinThreads = 50;
        private int _ListenerMinCompletionWorkThreads = 1000;

        public class Factory : BaseTransactionEngine.Factory
        {

        }
    }
}
