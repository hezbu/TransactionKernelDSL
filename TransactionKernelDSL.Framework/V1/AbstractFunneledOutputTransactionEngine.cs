using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using TransactionKernelDSL.Framework.Properties;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
    public delegate void UponReceptionCallBack(AbstractTransactionParserStructure structure);
    public abstract class AbstractFunneledOutputTransactionEngine : AbstractOutputTransactionEngine
    {
        #region Member Fields
        private Thread _SendWorker = null;
        private Thread _ReceiveWorker = null;
        /// <summary>
        /// Indica si el motor esta en un estado tal que puede reenviar mensajes o no
        /// </summary>
        protected bool _IsForwarding = false;

        private ThreadParamlessWorkerDelegate _SendWorkerMethod = null;
        private ThreadParamlessWorkerDelegate _ReceiveWorkerMethod = null;

        private AutoResetEvent _OnSendWorkerEvent = null;
        //private AutoResetEvent _OnReceiveWorkerAutoResetEvent = null; 
        private event UponReceptionCallBack _OnReceiveWorkerEvent = null;

        protected int _ResolveTimeout = 60;


        #endregion

        #region Member Properties
        public override bool IsEngineOn
        {
            get { return _IsForwarding; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        protected AbstractFunneledOutputTransactionEngine()
            : base()
        {
            Link(
                SendWorker,
                ReceiveWorker
                );

            _OnSendWorkerEvent = new AutoResetEvent(false);
            //     _OnReceiveWorkerEvent = new AutoResetEvent(false);
        }
        #endregion

        #region Available Inputs Methods

        public virtual void AddObserver(UponReceptionCallBack observer)
        {
            if (_OnReceiveWorkerEvent == null) _OnReceiveWorkerEvent = observer;
            else _OnReceiveWorkerEvent += observer;
        }
        public virtual void RemoveObserver(UponReceptionCallBack observer)
        {
            _OnReceiveWorkerEvent -= observer;
        }

        /// <summary>
        /// Vincula los delegados principales del motor con las funciones que se pasan como parámetro
        /// </summary>
        public virtual void Link(ThreadParamlessWorkerDelegate sendWorkerMethod, ThreadParamlessWorkerDelegate receiveWorkerMethod)
        {
            _SendWorkerMethod = sendWorkerMethod;
            _ReceiveWorkerMethod = receiveWorkerMethod;
        }

        public override bool Start()
        {
            if (this._SendWorker == null && this._ReceiveWorker == null)
            {
                _SendWorker = new Thread(() => _SendWorkerMethod());
                _ReceiveWorker = new Thread(() => _ReceiveWorkerMethod());

                _Log.Info("Starting ReceiveWorker...");
                this._ReceiveWorker.Start();
                _Log.Info("Starting SendWorker...");
                this._SendWorker.Start();

                return this._SendWorker.IsAlive && this._ReceiveWorker.IsAlive;
            }
            else return false;
        }
        public override bool Stop()
        {
            if (this._SendWorker != null && this._ReceiveWorker != null)
            {
                if (this._SendWorker.IsAlive && this._ReceiveWorker.IsAlive)
                {
                    #region Signalling 'Finish order' on workers stage
                    _Log.Info("Stopping SendWorker...");
                    this._SendWorker.Abort();
                    _Log.Info("Stopping ReceiveWorker...");
                    this._ReceiveWorker.Abort();
                    #endregion

                    #region Joining workers stage
                    _SendWorker.Join();
                    _ReceiveWorker.Join();
                    #endregion

                    this._SendWorker = this._ReceiveWorker = null;

                    return true;
                }
                else return false;
            }
            else return false;
        }
        /// <summary>
        /// Mecanismo de entrada al motor. Debe pasarse como argumento un Parser con los datos a ser enviados por el motor
        /// </summary>
        public override TransmissionStatus Resolve(AbstractTransactionParser parser)
        {
            TransmissionStatus retVal = TransmissionStatus.NoError;
            Observer obs = new Observer();
            if (_IsForwarding == true)
            {
                if (parser.AssembleMethod == null || parser.AssembleMethod() == true)
                {
                    if (PushRequirement(parser) == true)
                    {
                        _Log.Debug("Signaling SendWorkerThread...");
                        if (_OnSendWorkerEvent.Set() == true)
                        {
                            AddObserver(obs.OnReceive);
                            try
                            {
                                if (MustWaitForResponse(parser) == true)
                                {
                                    #region Wait for replies flow
                                    bool responseFound = false;
                                    DateTime dtStart = DateTime.Now;
                                    int resolveTimeout = _ResolveTimeout;

                                    do
                                    {
                                        _Log.Debug("Waiting for OnReceiveWorkerEvent for " + resolveTimeout  + "s... ");
                                        if (obs.OnReceiveWorkerAutoResetEvent.WaitOne((resolveTimeout * 1000 > 0 ? resolveTimeout * 1000 : 0), false) == true)                                        
                                        {
                                            if (PullResponse(parser) == true)
                                            {
                                                #region Dissasemble Control Flow
                                                if (parser.DisassembleMethod == null || parser.DisassembleMethod() == true)
                                                {
                                                    retVal = TransmissionStatus.NoError;
                                                    responseFound = true;
                                                }
                                                else
                                                {
                                                    _Log.Error("Error during DisassembleMethod().");
                                                    retVal = TransmissionStatus.BadDisassembling;
                                                }
                                                #endregion
                                            }
                                            else
                                            {
                                                resolveTimeout = resolveTimeout - (int)(DateTime.Now - dtStart).TotalSeconds;
                                                _Log.Debug("Not for me, resetting timeout counter to " + resolveTimeout + "s");
                                            }
                                        }
                                        else
                                        {
                                            _Log.Error("Timeout detected, transaction has no reponse");
                                            retVal = TransmissionStatus.Timeout;
                                            break;
                                        }
                                    }
                                    while (responseFound == false);
                                    #endregion
                                }
                                else
                                {
                                    _Log.Info("Transaction sent OK, must not wait for reply");
                                    retVal = TransmissionStatus.NoError;
                                }
                            }
                            catch (Exception ex)
                            {
                                _Log.Fatal("Exception found in Resolve(): " + ex.Message);
                                _Log.Fatal("Stack trace is: " + ex.StackTrace);
                                retVal = TransmissionStatus.ProblemDuringContact;
                            }
                            finally
                            {
                                RemoveObserver(obs.OnReceive);
                            }
                        }
                        else
                        {
                            _Log.Error("Error during _OnSendWorkerEvent.Set().");
                            retVal = TransmissionStatus.SendingError;
                        }
                    }
                    else
                    {
                        _Log.Error("Error during PushRequirement().");
                        retVal = TransmissionStatus.SendingError;
                    }
                }
                else
                {
                    _Log.Error("Error during AssembleMethod().");
                    retVal = TransmissionStatus.BadAssembling;
                }
            }
            else
            {
                _Log.Error("Cannot forward messages, engine is not ready");
                retVal = TransmissionStatus.SessionError;
            }

            return retVal;
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Debe sobrecargarse para colocar en un repositorio (DB, Shared Memory, etc) las tramas que saldran del motor
        /// </summary>
        protected abstract bool PushRequirement(AbstractTransactionParser parser);
        /// <summary>
        /// Debe sobrecargarse para esperar las tramas entrantes del motor, e identificar si pertenecen o no a la trama saliente asociada a la transacción invocante.
        /// </summary>
        protected abstract bool PullResponse(AbstractTransactionParser parser);
        /// <summary>
        /// Debe sobrecargarse para obtener desde un repositorio (DB, Shared Memory, etc) las tramas salientes que serán enviadas por el motor.
        /// </summary>
        protected abstract List<byte[]> PullAvailableRequirements();
        /// <summary>
        /// Must be overridden in order to send and outcoming message
        /// </summary>
        protected abstract bool SetRequirement();
        /// <summary>
        /// Debe sobrecargarse para validar las condiciones necesarias para esperar o no una respuesta.
        /// Este método se utiliza en aquellos casos que se requiere enviar un requerimiento a traves del motor, y
        /// no se debe esperar ninguna trama de respuesta.
        /// </summary>
        /// <param name="parser">Instancia valida de AbstractTransactionParser con los datos para decidir si espera o no respuesta</param>
        /// <returns>TRUE si debe esperar respuesta, FALSE en caso contrario</returns>
        protected abstract bool MustWaitForResponse(AbstractTransactionParser parser);
        /// <summary>
        /// Debe sobrecargarse para poder recibir tramas desde un nodo externo, devolviendo una colección de mensajes.
        /// </summary>
        protected abstract List<byte[]> GetAvailableResponses();
        /// <summary>
        /// Debe sobrecargarse para colocar en un repositorio (DB, Shared Memory, etc) las tramas entrantes que llegan al motor
        /// </summary>
        protected abstract bool PushResponse(AbstractTransactionParser parser);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        protected abstract bool ProcessResponse(AbstractTransactionParser parser);
        /// <summary>
        /// Debe sobrecargarse para realizar mantenimiento de la trama saliente del motor,  una vez que es enviada. Si no es necesario hacer ningún tipo de mantenimiento, debe sobrecargarse con "return true;"
        /// </summary>
        /// <returns></returns>
        protected abstract bool UponForwardProcess();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract AbstractTransactionParser ProcessPoolWorkerParserFactory();
        #endregion

        #region Engine Workers
        /// <summary>
        /// Worker por defecto del motor Funnelled para envíos
        /// </summary>
        protected void SendWorker()
        {
            bool isSenderWorking = true;
            _EngineLog.Info("SendWorker started Ok!");
            while (isSenderWorking)
            {
                try
                {
                    #region Something to send Flow
                    if (_OnSendWorkerEvent.WaitOne(2500, false) == true)
                    {
                        _EngineLog.Info("SendWorker signaled! Looking for new messages...");
                        foreach (byte[] msg in PullAvailableRequirements())
                        {
                            _AssignedParser.ResponseStream.Set(msg);

                            if (SetRequirement() == true)
                            {
                                if (UponForwardProcess() == true)
                                {

                                }
                                else
                                {
                                    _EngineLog.Error("SendWorker couldn't perform upon-forward process.");
                                }
                            }
                            else
                            {
                                _EngineLog.Error("SendWorker couldn't send the pending message.");
                            }
                        }
                    }
                    #endregion
                }
                catch (ThreadAbortException)
                {
                    isSenderWorking = false;
                    _EngineLog.Info("Finishing SendWorker...");
                    continue;
                }
                catch (Exception ex)
                {
                    _EngineLog.Fatal(Resources.ExceptionMessage + " " + ex.Message);
                    _EngineLog.Fatal(Resources.ExceptionStackTrace + " " + ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// Worker por defecto del motor Funnelled para recepción
        /// </summary>
        protected void ReceiveWorker()
        {
            bool isReceiverWorking = true;
            _EngineLog.Info("ReceiveWorker started Ok!");
            while (isReceiverWorking)
            {
                try
                {
                    foreach (byte[] msg in GetAvailableResponses())
                    {
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessPoolWorker), msg);
                    }
                }
                catch (ThreadAbortException)
                {
                    isReceiverWorking = false;
                    _EngineLog.Info("Finishing ReceiveWorker...");
                    continue;
                }
                catch (Exception ex)
                {
                    _EngineLog.Fatal(Resources.ExceptionMessage + " " + ex.Message);
                    _EngineLog.Fatal(Resources.ExceptionStackTrace + " " + ex.StackTrace);
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }

        }

        /// <summary>
        /// Pool Worker por defecto, para procesar tramas entrantes
        /// </summary>
        /// <param name="state"></param>
        protected void ProcessPoolWorker(object msg)
        {
            AbstractTransactionParser processPoolParser = ProcessPoolWorkerParserFactory();
            processPoolParser.RequestStream.Set(msg as byte[]);

            if (processPoolParser.DisassembleMethod != null && processPoolParser.DisassembleMethod() == true)
            //if (processPoolParser.Disassemble() == true)
            {
                if (ProcessResponse(processPoolParser) == true)
                {
                    if (PushResponse(processPoolParser) == true) //ORIG
                    { //ORIG
                        _Log.Debug("Signalling OnReceiveWorkerEvent to all threads...");
                        //_OnReceiveWorkerEvent.Set(); //ORIG
                        if (_OnReceiveWorkerEvent != null) _OnReceiveWorkerEvent(processPoolParser.RequestStructure);
                        else _Log.Error("No thread is listening OnReceiveWorkerEvent. ProcessPoolWorker() will not signal anyone.");
                    } //ORIG
                }
            }
            else
            {
                _EngineLog.Error("Error during disassembling of incoming data: " + " " + processPoolParser.ErrorMessage + "(" + processPoolParser.Status + ")");
            }
        }

        #endregion

        public class Observer
        {
            public AutoResetEvent OnReceiveWorkerAutoResetEvent = null;
            public Observer()
            {
                OnReceiveWorkerAutoResetEvent = new AutoResetEvent(false);
            }
            public void OnReceive(AbstractTransactionParserStructure structure)
            {
                OnReceiveWorkerAutoResetEvent.Set();
            }
        }
    }
}
