using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using log4net;

namespace TransactionKernelDSL.Framework.V1
{
    public class AbstractTransactionHandler
    {
        #region Member Fields
        protected object _State = null;
        //      protected AbstractTransactionNexus _AssignedNexus = null;
        protected object _AssignedClient = null;
        protected AbstractTransactionParser _AssignedParser = null;
        protected AbstractTransactionContext _Context = null;

        protected AbstractTransactionHandler _ForwardHandler = null;
        protected AbstractTransactionHandler _MaintenanceHandler = null;

        protected bool _FirstStageEnabled = true;
        protected bool _SecondStageEnabled = true;
        protected bool _ThirdStageEnabled = true;

        #region ITransactionHandlerForwardable Delegates Fields
        public BuildRequirementDelegate BuildRequirementMethod = null;
        public ResolveDelegate ResolveMethod = null;
        public GetResponseDelegate GetResponseMethod = null;
        public ForwardHandlerFactoryDelegate ForwardHandlerFactoryMethod = null;
        public ProcessTransactionDelegate ProcessTransactionMethod = null;
        #endregion

        #region ITransactionHandlerPersistable Delegates Fields
        public PreProcessTransactionDelegate PreProcessTransactionMethod = null;
        public PostProcessTransactionDelegate PostProcessTransactionMethod = null;
        public FinalPostProcessTransactionDelegate FinalPostProcessTransactionMethod = null;
        #endregion

        #region ITransactionHandlerListenable Delegates Fields
        public GetRequirementDelegate GetRequirementMethod = null;
        public BuildResponseDelegate BuildResponseMethod = null;
        public ReplyDelegate ReplyMethod = null;
        #endregion

        #region ITransactionHandlerMaintenanceable Delegates Fields
        public DoMaintenanceDelegate DoMaintenanceMethod = null;
        public MaintenanceHandlerFactoryDelegate MaintenanceHandlerFactoryMethod = null;
        #endregion

        protected ILog _Log = null;
        #endregion


        #region Member Properties
        public AbstractTransactionParser Parser
        {
            get
            {
                return this._AssignedParser;
            }
            set
            {
                _AssignedParser = value;
            }
        }

        public object Client
        {
            get
            {
                return this._AssignedClient;
            }
            set
            {
                _AssignedClient = value;
            }
        }

        public string Logger
        {
            set
            {
                _Log = LogManager.GetLogger(value);
            }
        }

        #endregion


        #region Constructor
        public AbstractTransactionHandler(AbstractTransactionContext context = null)
        {
            _Context = context;
            _Log = LogManager.GetLogger("MainLogger");
        }
        #endregion

        #region Member Virtual And Abstract Methods
        public virtual void DoTransaction(object state = null)
        {
            _State = state;
            if (_FirstStageEnabled == true) DoFirstStage();
            else _Log.Debug("First stage disabled!");
            if (_SecondStageEnabled == true) DoSecondStage();
            else _Log.Debug("Second stage disabled!");
            if (_ThirdStageEnabled == true) DoThirdStage();
            else _Log.Debug("Third stage disabled!");
        }

        protected virtual void DoFirstStage()
        {
            try
            {
                if (((GetRequirementMethod == null) ? true : GetRequirementMethod()))
                {
                    if (((PreProcessTransactionMethod == null) ? true : PreProcessTransactionMethod()))
                    {
                        //if (((ForwardHandlerFactoryMethod == null) ? true : ForwardHandlerFactoryMethod()))
                        //{

                        //}
                        //else _Log.Debug("ForwardHandlerFactoryMethod() returned false");
                    }
                    else _Log.Debug("PreProcessTransactionMethod() returned false");
                }
                else _Log.Debug("GetRequirementMethod() returned false");
            }
            catch (Exception ex)
            {
                _Log.Fatal("Exception found in First Stage: " + ex.Message);
                _Log.Fatal("SS: " + ex.StackTrace);
            }
        }
        protected virtual void DoSecondStage()
        {
            try
            {
                #region "ForwardHandlerFactory" Stage
                if (((ForwardHandlerFactoryMethod == null) ? true : ForwardHandlerFactoryMethod()))
                {
                    #region "ForwardHandler" Stage
                    if (this._ForwardHandler == null)
                    {
                        #region "BuildRequirement" Stage
                        if (((BuildRequirementMethod == null) ? true : BuildRequirementMethod()))
                        {
                            #region "Resolve" Stage
                            if (((ResolveMethod == null) ? true : ResolveMethod()))
                            {
                                #region "GetResponse" Stage
                                if (((GetResponseMethod == null) ? true : GetResponseMethod()))
                                {

                                }
                                else _Log.Debug("GetResponseMethod() returned false");
                                #endregion
                            }
                            else
                            {
                                _Log.Debug("ResolveMethod() returned false");
                            }
                            #endregion
                        }
                        else _Log.Debug("BuildRequirementMethod() returned false");
                        #endregion
                        #region "ProcessTransaction" Stage
                        if (((ProcessTransactionMethod == null) ? true : ProcessTransactionMethod()))
                        {

                        }
                        else _Log.Debug("ProcessTransactionMethod() returned false");
                        #endregion
                    }
                    else
                    {
                        this._ForwardHandler.DoTransaction(this);
                    } 
                    #endregion
                }
                else _Log.Debug("ForwardHandlerFactoryMethod() returned false"); 
                #endregion
            }
            catch (Exception ex)
            {
                _Log.Fatal("Exception found in Second Stage: " + ex.Message);
                _Log.Fatal("SS: " + ex.StackTrace);
            }
        }
        protected virtual void DoThirdStage()
        {
            try
            {
                if (((BuildResponseMethod == null) ? true : BuildResponseMethod()))
                {
                    if (((ReplyMethod == null) ? true : ReplyMethod()))
                    {

                    }
                    else _Log.Debug("ReplyMethod() returned false");
                }
                else _Log.Debug("BuildResponseMethod() returned false");


                if (((PostProcessTransactionMethod == null) ? true : PostProcessTransactionMethod()))
                {

                }
                else _Log.Debug("PostProcessTransactionMethod() returned false");

                if (((MaintenanceHandlerFactoryMethod == null) ? true : MaintenanceHandlerFactoryMethod()))
                {
                    if (((DoMaintenanceMethod == null) ? true : DoMaintenanceMethod()))
                    {

                    }
                    else _Log.Debug("DoMaintenanceMethod() returned false");
                }
                else _Log.Debug("MaintenanceHandlerFactoryMethod() returned false");

                if (((FinalPostProcessTransactionMethod == null) ? true : FinalPostProcessTransactionMethod()))
                {

                }
                else _Log.Debug("FinalPostProcessTransactionMethod() returned false");
            }
            catch (Exception ex)
            {
                _Log.Fatal("Exception found in Third Stage: " + ex.Message);
                _Log.Fatal("SS: " + ex.StackTrace);
            }

        }
        #endregion

    }
}
