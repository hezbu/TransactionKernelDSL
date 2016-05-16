using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractStraightOutputTransactionEngine : AbstractOutputTransactionEngine
    {
        #region Member Field
        protected bool _IsForwarding = false;
        #endregion

        #region Constructor
        protected AbstractStraightOutputTransactionEngine()
            : base()
        {
            
        }
        #endregion

        protected abstract IDisposable OutputHandlerFactory(object state = null);
        protected abstract bool Connect(object handler);
        protected abstract bool Disconnect(object handler);


        public override TransmissionStatus Resolve(AbstractTransactionParser parser)
        {
            TransmissionStatus retVal = TransmissionStatus.NoError;

            #region Session Control Flow
            if (_IsForwarding == true)
            {
                #region Assemble Control Flow
                if (parser.AssembleMethod == null || parser.AssembleMethod() == true)
                {
                    #region Output Handler Factory Control Flow
                    using (IDisposable outputHandler = OutputHandlerFactory(parser))
                    {
                        #region Connect control flow
                        if (this.Connect(outputHandler) == true)
                        {
                            #region Send Control Flow
                            if (parser.SendMethod == null || parser.SendMethod(outputHandler) == true)
                            {
                                #region Receive Control Flow
                                if (parser.ReceiveMethod == null || parser.ReceiveMethod(outputHandler) == true)
                                {
                                    #region Disconnect Control Flow
                                    if (this.Disconnect(outputHandler) == true)
                                    {
                                        #region Dissasemble Control Flow
                                        if (parser.DisassembleMethod == null || parser.DisassembleMethod() == true)
                                        {
                                            retVal = TransmissionStatus.NoError;
                                        }
                                        else
                                        {                                            
                                            _Log.Error("Error during DisassembleMethod(). " + parser.ErrorMessage + "(" + parser.Status + ")");
                                            retVal = TransmissionStatus.BadDisassembling;
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        _Log.Error("Error during Disconnect().");
                                        retVal = TransmissionStatus.DisconnectionError;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    _Log.Error("Error during ReceiveMethod().");
                                    retVal = parser.Status;
                                }
                                #endregion
                            }
                            else
                            {
                                _Log.Error("Error during SendMethod().");
                                retVal = TransmissionStatus.SendingError;
                            }
                            #endregion
                        }
                        else
                        {
                            _Log.Error("Error during Connect().");
                            retVal = TransmissionStatus.ConnectionError;
                        }
                        #endregion
                    }
                    #endregion
                }
                else
                {                    
                    _Log.Error("Error during AssembleMethod(). "+parser.ErrorMessage + "(" + parser.Status + ")");
                    retVal = TransmissionStatus.BadAssembling;
                } 
                #endregion
            }
            else
            {
                _Log.Error("Cannot forward messages, engine is not ready");
                retVal = TransmissionStatus.SessionError;
            } 
            #endregion

            return retVal;
        }

        public override bool IsEngineOn
        {
            get { return _IsForwarding; }
        }

        public override bool Start()
        {
            return (_IsForwarding = true) == true;
        }

        public override bool Stop()
        {
            return (_IsForwarding = false) == false;
        }



    }
}
