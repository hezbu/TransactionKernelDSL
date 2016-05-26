using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract partial class AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {
        protected TcpListener _Listener = null;
        protected int _ListenerTimeout = 60;
        protected IPAddress _ListenerIpAddress = IPAddress.Parse("127.0.0.1");
        protected int _ListenerTcpPort = 8888;
        protected int _ListenerMinThreads = 50;
        protected int _ListenerMinCompletionWorkThreads = 1000;
        protected Thread _TrackThread = null;

        protected int _PushSleep = 1000;
        protected int _TrackerSleep = 45000;
    
        protected AutoResetEvent _AutoTrackerEvent = null;
        protected event OnTrackSuccess _OnTrackSuccessEvent = null;

        public virtual int PushSleep { set { _PushSleep = value; } }

        public virtual int ListenerTimeout { set { _ListenerTimeout = value; } }
        public virtual IPAddress ListenerIpAddress { set { _ListenerIpAddress = value; } }
        public virtual int ListenerTcpPort { set { _ListenerTcpPort = value; } }
        public virtual int ListenerMinThreads { set { _ListenerMinThreads = value; } }
        public virtual int ListenerMinCompletionWorkThreads { set { _ListenerMinCompletionWorkThreads = value; } }

        protected AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine()
            : base()
        {
            Link(PushPullListenerTransactionEngineMaster, PushPullListenerTransactionEngineSlaveObserver);
            _TrackThread = new Thread(() => TrackThreadWorker());
            _AutoTrackerEvent = new AutoResetEvent(false);            
        }

      
        public override bool Start()
        {
            if (_IsListening == false)
            {
                 _IsListening = base.Start();
                 if (_IsListening)
                 {
                     _TrackThread.Start();
                 }
                 return _IsListening;
            }
            else return false;
        }

        public override bool Stop()
        {
            if (_IsListening == true)
            {
                _IsListening = false;
                _TrackThread.Join();
                _Log.InfoFormat("TrackThreadWorker joined OK!");
                return base.Stop();
            }
            else return false;
        }


        protected virtual string Actors(TcpClient client)
        {
            try
            {
                return client.Client.LocalEndPoint.ToString() + " - " + client.Client.RemoteEndPoint.ToString();
            }
            catch (Exception)
            {
                return "Unknown actors(exception catched)";
            }
        }

    }
}
