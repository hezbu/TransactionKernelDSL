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
    public delegate void OnTrackSuccess(AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine.TrackerWorkerInfo info);
    public abstract class AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {
        protected TcpListener _Listener = null;
        protected int _ListenerTimeout = 60;
        protected IPAddress _ListenerIpAddress = IPAddress.Parse("127.0.0.1");
        protected int _ListenerTcpPort = 8888;
        protected int _ListenerMinThreads = 50;
        protected int _ListenerMinCompletionWorkThreads = 1000;
        protected Thread _TrackThread = null;

        protected int _PushSleep = 1000;
        protected int _TrackerSleep = 4000;
    
        protected AutoResetEvent _AutoTrackerEvent = null;
        protected ConcurrentBag<TrackerWorkerInfo> _ConcurrentBag = null;

        public virtual int PushSleep { set { _PushSleep = value; } }

        public virtual int ListenerTimeout { set { _ListenerTimeout = value; } }
        public virtual IPAddress ListenerIpAddress { set { _ListenerIpAddress = value; } }
        public virtual int ListenerTcpPort { set { _ListenerTcpPort = value; } }
        public virtual int ListenerMinThreads { set { _ListenerMinThreads = value; } }
        public virtual int ListenerMinCompletionWorkThreads { set { _ListenerMinCompletionWorkThreads = value; } }

        protected AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine()
            : base()
        {
            Link(PushPullListenerTransactionEngineMaster, PushPullListenerTransactionEngineSlave);
            _TrackThread = new Thread(() => TrackThreadWorker());
            _AutoTrackerEvent = new AutoResetEvent(false);
            _ConcurrentBag = new ConcurrentBag<TrackerWorkerInfo>();
        }

        protected void TrackThreadWorker()
        {
            try
            {
                _Log.InfoFormat("TrackThreadWorker on..");
                while (_IsListening)
                {
                    var trackerInfo = DoTrackerWork();
                    if (trackerInfo != null)
                    {
                        if (_ConcurrentBag.Count > 0)
                        {
                            TrackerWorkerInfo lastTrackerInfo = null;
                            while (_ConcurrentBag.TryTake(out lastTrackerInfo) == false)
                            {
                                _Log.ErrorFormat("Error taking out last");
                            }
                        }

                        _ConcurrentBag.Add(trackerInfo);
                        _AutoTrackerEvent.Set();
                    }
                    Thread.Sleep(_TrackerSleep);
                }
            }
            catch (Exception ex)
            {
                _Log.FatalFormat("Exception found at TrackThreadWorker: {0}", ex);

            }
            finally
            {
                _Log.InfoFormat("TrackThreadWorker off..");
            }
        }

        protected abstract TrackerWorkerInfo DoTrackerWork();

        public class TrackerWorkerInfo
        {

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

        protected void PushPullListenerTransactionEngineMaster()
        {
            try
            {
                _Listener = new TcpListener(_ListenerIpAddress, _ListenerTcpPort);

                _Log.Info("IpAddressServer: " + ((IPEndPoint)_Listener.LocalEndpoint).Address.ToString() +
                            " Listen Tcp Port: " + ((IPEndPoint)_Listener.LocalEndpoint).Port.ToString());

                //EB; 29/05/2013
                int workerThreadsMax, completionPortThreadsMax;
                int workerThreadsMin, completionPortThreadsMin;

                ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);
                ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);


                _Log.Info("workerThreadsMin:{" + workerThreadsMin + "} ; completionPortThreadsMin:{" + completionPortThreadsMin +
                                    "} :: workerThreadsMax:{" + workerThreadsMax + "} ; completionPortThreadsMax:{" + completionPortThreadsMax + "}");

                //Establecer Parámetros de Trabajo del ThreadPool 
                if (ThreadPool.SetMaxThreads(_ListenerMinCompletionWorkThreads, _ListenerMinCompletionWorkThreads) == true)
                {
                    ThreadPool.GetMaxThreads(out workerThreadsMax, out completionPortThreadsMax);
                    _Log.Info("SetMaxThreads " + _ListenerMinCompletionWorkThreads.ToString() + " OK -> (" + workerThreadsMax.ToString() + "," + completionPortThreadsMax.ToString() + ")");
                }
                else
                {
                    _Log.Info("SetMaxThreads " + _ListenerMinCompletionWorkThreads.ToString() + " ERROR");
                }

                //Establecer Parámetros de Trabajo del ThreadPool 
                if (ThreadPool.SetMinThreads(_ListenerMinThreads, _ListenerMinCompletionWorkThreads) == true)
                {
                    ThreadPool.GetMinThreads(out workerThreadsMin, out completionPortThreadsMin);
                    _Log.Info("SetMinThreads " + _ListenerMinThreads.ToString() + " OK -> (" + workerThreadsMin.ToString() + "," + completionPortThreadsMin.ToString() + ")");
                }
                else
                {
                    _Log.Info("SetMinThreads " + _ListenerMinThreads.ToString() + " ERROR");
                }

                _Listener.Start();

                _IsListening = true;
                do
                {
                    if (!_Listener.Pending())
                    {
                        Thread.Sleep(100);
                        continue;
                    }

                    TcpClient tcpclient = _Listener.AcceptTcpClient();

                    tcpclient.ReceiveTimeout = _ListenerTimeout * 1000;
                    tcpclient.SendTimeout = _ListenerTimeout * 1000;

                    if (ThreadPool.QueueUserWorkItem(new WaitCallback(_ThreadPoolWorkerDelegateMethod), tcpclient) == false)
                    {
                        _Log.Error("ERROR EN THREADPOOL QueueUserWorkItem");
                    }

                    //EB ; 29/05/2013
                    //Leer Estado Actual del Thread Pool, Luego de Encolar 1 Transacción
                    int workerThreadsCurrent = 0, completionPortThreadsCurrent = 0;
                    ThreadPool.GetAvailableThreads(out workerThreadsCurrent, out completionPortThreadsCurrent);
                    _Log.Info("ThreadPool Info." + " :: " + "workerThreadsCurrent:[" + ((workerThreadsMax >= workerThreadsCurrent) ? (workerThreadsMax - workerThreadsCurrent).ToString() : "Error") + "] ; completionPortThreadsCurrent:[" + ((completionPortThreadsMax >= completionPortThreadsCurrent) ? (completionPortThreadsMax - completionPortThreadsCurrent).ToString() : "Error") + "]"); //HZ v1.0.1.0
                }
                while (_IsListening);

            }
            catch (Exception ex)
            {
                _Log.Info("Error in: " + ex.Source + " Message: " + ex.Message);
            }
            finally
            {
                _Listener.Stop();
            }
        }
        //        protected void PushPullReceiverWorker(object slaveParam)
        //        {
        //            //AbstractTcpClientTransactionNexus listenerNexus = this.NexusFactory() as AbstractTcpClientTransactionNexus;
        //            AbstractTransactionParser parser = this.ParserFactory();

        //#if DEBUG == true
        //            Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 1");          
        //#endif
        //            using (TcpClient client = ((TcpClient)slaveParam))
        //            {
        //                try
        //                {
        //                    if (parser.ReceiveMethod != null && parser.ReceiveMethod(client) == true)
        //                    {

        //#if DEBUG == true
        //                        Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 2");                        
        //#endif

        //                        if (parser.IsKeepAliveMessageMethod != null && parser.IsKeepAliveMessageMethod() == false)
        //                        {

        //#if DEBUG == true
        //                            Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 3");
        //#endif

        //                            if (parser.DisassembleMethod != null && parser.DisassembleMethod() == true)
        //                            {

        //#if DEBUG == true
        //                                Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 4");                                
        //#endif
        //                                AbstractTransactionHandler transaction = TransactionHandlerFactory(parser.RequestStructure.GetOperationId());
        //                                transaction.Client = client;
        //                                transaction.Parser = parser;
        //                                transaction.DoTransaction();
        //                            }
        //                            else
        //                            {
        //                                _Log.Error("Error found during Disassemble() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");

        //                                if (parser.SendMethod == null || parser.SendMethod(client) == false)
        //                                    _Log.Error("Error found during Send() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
        //                            }
        //                        }

        //                        else if (parser.SendMethod == null || parser.SendMethod(client) == false)
        //                            _Log.Error("Error found during Send() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
        //                    }

        //                    else _Log.Error("Error found during Receive() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
        //                }
        //                catch (Exception ex)
        //                {
        //                    _Log.Error("Connection closed (Exception found): " + ex.Message + " (" + this.Actors(client) + ")");
        //                }
        //                finally
        //                {
        //#if DEBUG == true
        //                    Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 5");
        //#endif
        //                    try
        //                    {
        //                        if (client != null)
        //                        {
        //                            client.GetStream().Dispose();
        //                            client.Client.Close();
        //                            client.Close();
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        _Log.Error("Error during TcpClient disposal: " + ex.Message);
        //                    }

        //                    _Log.Info("Connection closed Ok");
        //                }
        //            }

        //        }

        protected void PushPullListenerTransactionEngineSlave(object slaveParam)
        {
            WorkerInfo workerInfo = new WorkerInfo();
            Thread senderWorker = new Thread(() => SendWorker(workerInfo));
            using (TcpClient client = (TcpClient)slaveParam)
            {
                while (_IsListening)
                {
                    try
                    {
                        AbstractTransactionParser parser = this.ParserFactory();
                        if (parser.ReceiveMethod != null && parser.ReceiveMethod(client) == true)
                        {

                            if (parser.IsKeepAliveMessageMethod != null && parser.IsKeepAliveMessageMethod() == false)
                            {

                                if (parser.DisassembleMethod != null && parser.DisassembleMethod() == true)
                                {
                                    AbstractTransactionHandler transaction = TransactionHandlerFactory(parser.RequestStructure.GetOperationId());
                                    transaction.Client = client;
                                    transaction.Parser = parser;
                                    workerInfo.Client = client;
                                    workerInfo.Parser = parser;
                                    transaction.DoTransaction(workerInfo);

                                    if (workerInfo.IsSenderWorkerOn == false)
                                    {
                                        workerInfo.ClientId = this.GetClientId(parser);
                                        if (workerInfo.ClientId != null)
                                        {
                                            workerInfo.IsSenderWorkerOn = true;
                                            senderWorker.Start();
                                        }
                                    }
                                }
                                else
                                {
                                    _Log.Error("Error found during Disassemble() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");

                                    if (parser.SendMethod == null || parser.SendMethod(client) == false)
                                        _Log.Error("Error found during Send() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
                                }
                            }

                            else if (parser.SendMethod == null || parser.SendMethod(client) == false)
                                _Log.Error("Error found during Send() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
                        }

                        else
                        {
                            _Log.Error("Error found during Receive() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
                            break;
                        }
                    }
                    catch (IOException ioEx)
                    {
                        continue;
                    }
                    catch (Exception ex)
                    {
                        _Log.ErrorFormat("Exception found at ReceiveWorker: {0}", ex.Message);
                        break;
                    }

                }

                try
                {
                    if (senderWorker != null && workerInfo.IsSenderWorkerOn)
                    {
                        _Log.InfoFormat("Waiting for sender worker to finish...");
                        workerInfo.IsSenderWorkerOn = false;
                        senderWorker.Join();
                        _Log.InfoFormat("Sender joined OK, finishing thread...");
                    }
                    else _Log.InfoFormat("Finishing thread...");
                }
                catch (Exception ex)
                {
                    _Log.ErrorFormat("Exception while joining sender worker: {0}", ex);
                }


            }
        }

        protected abstract string GetClientId(AbstractTransactionParser parser);


        protected void SendWorker(WorkerInfo workerInfo)
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
		        _Log.ErrorFormat("Exception found at SendWorker: {0}",ex);
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

    }
}
