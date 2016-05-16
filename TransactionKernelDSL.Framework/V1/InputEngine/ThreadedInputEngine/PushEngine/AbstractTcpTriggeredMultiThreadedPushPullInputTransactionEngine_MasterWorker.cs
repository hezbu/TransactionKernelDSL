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
    }
}
