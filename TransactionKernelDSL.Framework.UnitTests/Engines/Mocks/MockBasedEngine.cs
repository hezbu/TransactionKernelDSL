using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V2.BaseClasses;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Engines.Mocks
{
    public class MockBasedEngine : BaseTransactionEngine
    {

        internal MockBasedEngine()
        {
            
        }

      
        public override bool Start()
        {
            return base.Start();
        }

        public bool Stop()
        {
            return base.Stop();
        }
    }
}
