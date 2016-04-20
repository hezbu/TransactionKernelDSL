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
        private MockFacade()
        {
        }
       
        public bool StartEngines()
        {
            return true;
        }

        public bool StopEngines()
        {
            return true;
        }

        public static class Factory
        {
            private static MockFacade _Instance = null;

            public static MockFacade Build()
            {
                if (_Instance == null)
                {
                    _Instance = new MockFacade();
                }

                return _Instance;
            }
        }
    }

      
}
