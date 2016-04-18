using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTransactionClearing<T>
    {
        private List<T> _ProviderTransactions;
        private List<T> _OwnTransactions;

        private List<T> _NotFoundInProviderTransactions;
        private List<T> _NotFoundInOwnTransactions;

        private List<string> _ErrorsDuringClearingProcess;


        protected ILog _Log = null;
        protected string _ProcessName = null;
        protected string _ProviderName = null;
        protected string _ErrorMessage = null;

        protected abstract bool HasToRetrieveTransactions();
        protected abstract bool RetrieveProviderTransactions(List<T> lstTransactions);
        protected abstract bool RetrieveOwnTransactions(List<T> lstTransactions);
        protected abstract bool CreateClearingFile(List<T> lstTransactions);
        protected abstract bool PostProcessClearing(List<T> lstProviderTransactions, List<T> lstOwnTransactions, List<T> lstNotFoundInProviderTransactions, List<T> lstNotFoundInOwnTransactions);
        protected abstract bool PostProcessClearingFile();
        protected abstract bool HasToCreateClearingFileForProviderUse();


        protected virtual bool DoProviderClearing(List<T> lstProviderTransactions,
                                                    List<T> lstOwnTransactions,
                                                    List<T> lstNotFoundInProviderTransactions)
        {
            foreach (T transaction in lstProviderTransactions)
            {
                if (lstOwnTransactions.Find(t => t.Equals(transaction)) == null)
                {
                    lstNotFoundInProviderTransactions.Add(transaction);
                    this.AddClearingProcessErrorMessage("Transaction " + transaction.ToString() + " is in " + _ProviderName + " file, but is not in the record");
                }
            }

            return true;
        }
        protected virtual bool DoOwnClearing(List<T> lstProviderTransactions,
                                                List<T> lstOwnTransactions,
                                                List<T> lstNotFoundInOwnTransactions
                                            )
        {
            foreach (T transaction in lstOwnTransactions)
            {
                if (lstProviderTransactions.Find(t => t.Equals(transaction)) == null)
                {
                    lstNotFoundInOwnTransactions.Add(transaction);
                    this.AddClearingProcessErrorMessage("Transaction " + transaction.ToString() + " is in our file, but is not in " + _ProviderName + "r Record");
                }
            }

            return true;
        }


        protected virtual List<string> ErrorsDuringClearingProcess
        {
            get
            {
                return this._ErrorsDuringClearingProcess;
            }
        }

        protected virtual void AddClearingProcessErrorMessage(string errorMessage)
        {
            this._ErrorsDuringClearingProcess.Add(errorMessage);
            _Log.Error("Error #" + this._ErrorsDuringClearingProcess.Count + ": " + errorMessage);
        }

        protected virtual string Logger
        {
            set
            {
                _Log = LogManager.GetLogger(value);
            }
        }

        public AbstractTransactionClearing(
                                                string processName = "Generic Clearing Process",
                                                string providerName = "Provider"
                                            )
        {
            _ProcessName = processName;
            _ProviderName = providerName;
            _Log = LogManager.GetLogger("MainLogger");
            _ProviderTransactions = new List<T>();
            _OwnTransactions = new List<T>();
            _NotFoundInProviderTransactions = new List<T>();
            _NotFoundInOwnTransactions = new List<T>();

            _ErrorsDuringClearingProcess = new List<string>();
        }


        public bool DoClearing()
        {
            bool result = true;
            _Log.Info("Starting "+_ProviderName+" "+_ProcessName+"...");
            #region Retrieve Info Stage
            if (this.HasToRetrieveTransactions() == true)
            {
                _Log.Info("HasToRetrieveTransactions() returned OK, starting retrieving stages...");
                #region Retrieve Provider Transactions Stage
                if (this.RetrieveProviderTransactions(_ProviderTransactions) == true)
                {
                    _Log.Info("RetrieveProviderTransactions() stage finished OK!");
                    #region Retrieve Own Transactions Stage
                    if (this.RetrieveOwnTransactions(_OwnTransactions) == true)
                    {
                        _Log.Info("RetrieveOwnTransactions() stage finished OK!");

                        if (this.DoProviderClearing(_ProviderTransactions, _OwnTransactions, _NotFoundInProviderTransactions) == false)
                        {
                            _Log.Fatal(_ProcessName + " failed in step DoProviderClearing(): " + _ErrorMessage);
                            result = false;
                        }
                        else _Log.Info("DoProviderClearing() stage finished OK!");

                        if (this.DoOwnClearing(_ProviderTransactions, _OwnTransactions, _NotFoundInOwnTransactions) == false)
                        {
                            _Log.Fatal(_ProcessName + " failed in step DoOwnClearing(): " + _ErrorMessage);
                            result = false;
                        }
                        else _Log.Info("DoOwnClearing() stage finished OK!");

                        if (this.PostProcessClearing(_ProviderTransactions, _OwnTransactions, _NotFoundInProviderTransactions, _NotFoundInOwnTransactions) == false)
                        {
                            _Log.Fatal(_ProcessName + " failed in step PostProcessClearing(): " + _ErrorMessage);
                            result = false;
                        }
                        else _Log.Info("PostProcessClearing() stage finished OK!");
                    }
                    else
                    {
                        _Log.Fatal(_ProcessName + " failed in step RetrieveOwnTransactions(): " + _ErrorMessage);
                        result = false;
                    }
                    #endregion
                }
                else
                {
                    _Log.Fatal(_ProcessName + " failed in step RetrieveProviderTransactions(): " + _ErrorMessage);
                    result = false;
                }
                #endregion
            }
            else
            {
                _Log.Info(_ProcessName + " has not the need to retrieve transactions from provider");
            }
            #endregion
            #region Create Transaction File Stage
            if (this.HasToCreateClearingFileForProviderUse() == true)
            {
                _Log.Info("HasToRetrieveTransactions() returned OK, starting retrieving stages...");

                #region Retrieve Own Transactions Stage
                if (this.RetrieveOwnTransactions(_OwnTransactions) == true)
                {
                    _Log.Info("RetrieveOwnTransactions() stage finished OK!");

                    if (this.CreateClearingFile(_OwnTransactions) == true)
                    {
                        _Log.Info("CreateClearingFile() stage finished OK!");

                        if (this.PostProcessClearingFile() == false)
                        {
                            _Log.Fatal(_ProcessName + " failed in step PostProcessClearingFile(): " + _ErrorMessage);
                            result = false;
                        }
                        else _Log.Info("PostProcessClearing() stage finished OK!");
                    }
                    else
                    {
                        _Log.Fatal(_ProcessName + " failed in step CreateClearingFile(): " + _ErrorMessage);
                        result = false;
                    }
                }
                else
                {
                    _Log.Fatal(_ProcessName + " failed in step RetrieveOwnTransactions(): " + _ErrorMessage);
                    result = false;
                }
                #endregion
            }
            else
            {
                _Log.Info(_ProcessName + " has not the need to create a transaction file to be sent to the provider");
            }
            #endregion

            if (result == true)
            {
                _Log.Info("Clearing process finished OK, with " + this.ErrorsDuringClearingProcess.Count + " errors.");
            }
            else
            {
                _Log.Error("Clearing process failed, with " + this.ErrorsDuringClearingProcess.Count + " errors.");
            }
            
            return result;
        }
    }
}
