using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractInputTransactionEngine : AbstractTransactionEngine
    {
        public override EngineType Type
        {
            get { return EngineType.InputEngine; }
        }

        protected abstract AbstractTransactionHandler TransactionHandlerFactory(object transactionId);
        protected abstract AbstractTransactionParser ParserFactory(object state = null);

        public AbstractInputTransactionEngine()
            : base()
        {

        }

    }
}
