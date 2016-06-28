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
        private static object _EventLock = new object();
        protected abstract AbstractTrackerObserver ObserverFactory(ILog log, TcpClient client);
        protected abstract string GetClientId(AbstractTransactionParser parser);

        protected void PushPullListenerTransactionEngineSlaveObserver(object slaveParam)
        {
            using (TcpClient client = (TcpClient)slaveParam)
            {
                var observer = this.ObserverFactory(_Log, client);

                lock (_EventLock)
                {
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
                }

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
                                    var observerInfo = observer.BuildObserverInfo(client, parser);
                                    transaction.DoTransaction(observerInfo);
                                    observer.ProcessObserverInfo(observerInfo);

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
                                }
                            }
                            else
                            {
                                _Log.InfoFormat("Keep alive detected!");
                                continue;
                            }
                        }
                        else
                        {
                            _Log.ErrorFormat("Error in Receive() ({1}): {0}", parser.ErrorMessage, parser.Status);
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
