using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using TransactionKernelDSL.Framework.V2.BaseClasses;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Handler.Mocks
{
    public class MockTransactionHandler : BaseTransactionHandler
    {
        public MockTransactionHandler(  ITransactionContext ctx, 
                                        ITransactionParser parser,
                                        ILog log
                                    )
            : base(ctx, parser, log)
        {

        }

        public override string GetTransactionId()
        {
            return "Mock";
        }
    }
}
