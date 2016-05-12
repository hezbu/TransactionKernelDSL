using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTcpTriggeredMultiThreadedInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {

        protected TcpListener _Listener = null;
        protected int _ListenerTimeout = 60;
        protected IPAddress _ListenerIpAddress = IPAddress.Parse("127.0.0.1");
        protected int _ListenerTcpPort = 8888;
        protected int _ListenerMinThreads = 50;
        protected int _ListenerMinCompletionWorkThreads = 1000;

        public virtual int ListenerTimeout { set { _ListenerTimeout = value; } }
        public virtual IPAddress ListenerIpAddress { set { _ListenerIpAddress = value; } }
        public virtual int ListenerTcpPort { set { _ListenerTcpPort = value; } }
        public virtual int ListenerMinThreads { set { _ListenerMinThreads = value; } }
        public virtual int ListenerMinCompletionWorkThreads { set { _ListenerMinCompletionWorkThreads = value; } }

        protected AbstractTcpTriggeredMultiThreadedInputTransactionEngine()
            : base()
        {
            Link(MultiThreadedListenerTransactionEngineMaster, MultiThreadedListenerTransactionEngineSlave);
        }

        public override bool Start()
        {
            if (_IsListening == false)
            {
                return _IsListening = base.Start();
            }
            else return false;
        }

        public override bool Stop()
        {
            if (_IsListening == true)
            {
                _IsListening = false;
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


        protected void MultiThreadedListenerTransactionEngineMaster()
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
        protected void MultiThreadedListenerTransactionEngineSlave(object slaveParam)
        {
            //AbstractTcpClientTransactionNexus listenerNexus = this.NexusFactory() as AbstractTcpClientTransactionNexus;
            AbstractTransactionParser parser = this.ParserFactory();
            
#if DEBUG == true
            Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 1");          
#endif
            using (TcpClient client = ((TcpClient)slaveParam))
            {
                try
                {
                    if (parser.ReceiveMethod != null && parser.ReceiveMethod(client) == true)
                    {
                        
#if DEBUG == true
                        Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 2");                        
#endif
                        
                        if (parser.IsKeepAliveMessageMethod != null && parser.IsKeepAliveMessageMethod() == false)
                        {
                            
#if DEBUG == true
                            Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 3");
#endif

                            if (parser.DisassembleMethod != null && parser.DisassembleMethod() == true)
                            {
                                
#if DEBUG == true
                                Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 4");                                
#endif
                                AbstractTransactionHandler transaction = TransactionHandlerFactory(parser.RequestStructure.GetOperationId());
                                transaction.Client = client;
                                transaction.Parser = parser;
                                transaction.DoTransaction();
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
                    
                    else _Log.Error("Error found during Receive() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
                }
                catch (Exception ex)
                {
                    _Log.Error("Connection closed (Exception found): " + ex.Message + " (" + this.Actors(client) + ")");
                }
                finally
                {
#if DEBUG == true
                    Debug.WriteLine("[MultiThreadedListenerTransactionEngineSlave] Step 5");
#endif
                    try
                    {
                        if (client != null)
                        {
                            client.GetStream().Dispose();
                            client.Client.Close();
                            client.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        _Log.Error("Error during TcpClient disposal: " + ex.Message);
                    }

                    _Log.Info("Connection closed Ok");
                }
            }

        }
    }
}
