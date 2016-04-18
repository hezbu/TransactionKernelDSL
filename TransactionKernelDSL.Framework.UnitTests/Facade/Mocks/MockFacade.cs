using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.UnitTests.Facade.Mocks
{
    public class MockFacade : ITransactionFacade
    {
        public int InstanceId
        {
            get { return 99; }
        }

        public bool StartEngines()
        {
            return true;
        }

        public bool StopEngines()
        {
            return true;
        }
    }
}
