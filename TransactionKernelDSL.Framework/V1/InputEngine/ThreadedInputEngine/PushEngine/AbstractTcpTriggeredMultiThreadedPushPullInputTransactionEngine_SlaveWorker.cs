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
        protected abstract AbstractTrackerObserver ObserverFactory(ILog log, TcpClient client);
        protected abstract string GetClientId(AbstractTransactionParser parser);
        protected void PushPullListenerTransactionEngineSlave(object slaveParam)
        {
            WorkerInfo workerInfo = new WorkerInfo();
            Thread senderWorker = new Thread(() => ObserverWorker(workerInfo));
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
        protected void PushPullListenerTransactionEngineSlaveObserver(object slaveParam)
        {

            using (TcpClient client = (TcpClient)slaveParam)
            {
                var observer = this.ObserverFactory(_Log, client);

                if (_OnTrackSuccessEvent == null)
                {
                    _Log.InfoFormat("Attaching to OnTrackSuccessEvent for first time..");
                    _OnTrackSuccessEvent = observer.OnTrackSuccessEvent;
                }
                else
                {
                    _Log.InfoFormat("Attaching to OnTrackSuccessEvent..");
                    _OnTrackSuccessEvent += observer.OnTrackSuccessEvent;
                }

                while (_IsListening)
                {
                    try
                    {
                        AbstractTransactionParser parser = this.ParserFactory();
                        if (parser.ReceiveMethod != null && parser.ReceiveMethod(client) == true)
                        {
                            if (parser.DisassembleMethod != null && parser.DisassembleMethod() == true)
                            {
                                AbstractTransactionHandler transaction = TransactionHandlerFactory(parser.RequestStructure.GetOperationId());
                                transaction.Client = client;
                                transaction.Parser = parser;
                                var observerInfo = new TrackerObserverInfo(client, parser);
                                transaction.DoTransaction(observerInfo);

                                if (observer.ClientId == null)
                                {
                                    observer.ClientId = this.GetClientId(parser);
                                }

                                if (observerInfo.HasToSendReply)
                                {
                                    observer.SendReply(observerInfo);
                                }
                                else
                                {
                                    _Log.WarnFormat("Transaction defined NOT to send reply... avoiding SendReply step...");
                                }

                            }
                            else
                            {
                                _Log.Error("Error found during Disassemble() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");

                                if (parser.SendMethod == null || parser.SendMethod(client) == false)
                                    _Log.Error("Error found during Send() stage: " + parser.ErrorMessage + " (" + this.Actors(client) + ")");
                            }
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
                    if (_OnTrackSuccessEvent != null)
                    {
                        _Log.InfoFormat("Deattaching from OnTrackSuccessEvent..");
                        _OnTrackSuccessEvent -= observer.OnTrackSuccessEvent;
                    }
                }
                catch (Exception ex)
                {
                    _Log.ErrorFormat("Exception found at deattaching observer: {0}", ex.Message);
                }
            }


        }

        
    }
}
