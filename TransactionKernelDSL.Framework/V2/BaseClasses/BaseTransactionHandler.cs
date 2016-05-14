using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.V2.BaseClasses
{
    public abstract class BaseTransactionHandler : ITransactionHandler
    {
        private ITransactionContext _Context;
        private ITransactionParser _Parser;
        private ILog _Log;

        protected BaseTransactionHandler(ITransactionContext context, 
                                         ITransactionParser parser,
                                         ILog log
                                        )
        {
            _Context = context;
            _Parser = parser;
            _Log = log;
        }

        public ITransactionContext DoTransaction()
        {
            return _Context;
        }

        public abstract string GetTransactionId();
    }
}
