using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate void OnTrackSuccess(object sender, TransactionKernelDSL.Framework.V1.AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine.OnTrackArgs args);


    public abstract partial class AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {
        protected void ObserverWorker(WorkerInfo workerInfo)
        {
            _Log.InfoFormat("Send Worker on!");
            try
            {
                if (workerInfo.ClientId == null)
                {
                    _Log.ErrorFormat("Client ID is null, sender worker will be stopped...");
                }
                else
                {
                    while (_IsListening && workerInfo.IsSenderWorkerOn)
                    {
                        //if (_AutoTrackerEvent.WaitOne(10000))
                        //{
                        //    _Log.InfoFormat("_AutoTrackerEvent TRUE");
                        //}

                        Thread.Sleep(10000);
                    }
                }
                _Log.InfoFormat("Send Worker off!");
            }
            catch (Exception ex)
            {
                _Log.ErrorFormat("Exception found at SendWorker: {0}", ex);
            }
        }

        public class WorkerInfo
        {
            public TcpClient Client { get; set; }

            public AbstractTransactionParser Parser { get; set; }

            public bool IsSenderWorkerOn { get; set; }

            public string ClientId { get; set; }

            public WorkerInfo()
            {
                IsSenderWorkerOn = false;
            }
        }


        public abstract class AbstractTrackerObserver
        {
            protected ILog _Log = null;
            public string ClientId { get; set; }
            protected object _SendLock = new object();
            protected TcpClient _Client;

            public AbstractTrackerObserver(ILog log, TcpClient client)
            {              
                this._Log = log;
                this._Client = client;
            }

            public abstract AbstractTransactionParser GetParserForEventMessage();

            public void OnTrackSuccessEvent(object sender, OnTrackArgs args)
            {
                try
                {
                    if (ClientId != null)
                    {
                        if (args.Items.Where(t => t.TrackedClientId == ClientId).Any() == true)
                        {
                            _Log.InfoFormat("OnTrackSuccessEvent! ClientId={0} - EventArgs={1}", ClientId ?? "N/A", args);

                            var observerInfo = new TrackerObserverInfo(_Client, GetParserForEventMessage());

                            if (this.SendReply(observerInfo) == false)
                            {
                                _Log.ErrorFormat("Error sending push info {0}", observerInfo.Parser.ErrorMessage);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _Log.FatalFormat("Exception found while sending push info {0}", ex);
                }
            }

            public bool SendReply(TrackerObserverInfo observerInfo)
            {
                lock (this._SendLock)
                {
                    if (observerInfo.Parser.SendMethod != null && observerInfo.Parser.SendMethod(observerInfo.Client) == true)
                    {
                        return true;
                    }
                    else
                    {
                        _Log.ErrorFormat("Error found during Reply() stage: {0} ({1} - {2})", observerInfo.Parser.ErrorMessage, (observerInfo.Client as System.Net.Sockets.TcpClient).Client.LocalEndPoint, (observerInfo.Client as System.Net.Sockets.TcpClient).Client.RemoteEndPoint);
                        return false;
                    }
                }

            }
        }

        public class OnTrackArgs : EventArgs
        {
            public List<OnTrackArgsItem> Items { get; set; }

            public OnTrackArgs()
            {
                Items = new List<OnTrackArgsItem>();
            }

            public override string ToString()
            {
                var result = String.Empty;
                foreach (var i in Items)
                {
                    result += String.Format("{0},", i);
                }
                return result;
            }
        }

        public class OnTrackArgsItem
        {
            public DateTime TrackedDateTime { get; set; }
            public string TrackedClientId { get; set; }

            public override string ToString()
            {
                return String.Format("[DT: {0}, CID: {1}]", TrackedDateTime, TrackedClientId);
            }
        }

        public class TrackerObserverInfo
        {
            private TcpClient _Client;
            private AbstractTransactionParser _Parser;

            public TcpClient Client { get { return _Client; } }
            public AbstractTransactionParser Parser { get { return _Parser; } }

            public TrackerObserverInfo(TcpClient client, AbstractTransactionParser parser)
            {
                this._Client = client;
                this._Parser = parser;
            }

            public bool HasToSendReply { get; set; }
        }
    }


}
