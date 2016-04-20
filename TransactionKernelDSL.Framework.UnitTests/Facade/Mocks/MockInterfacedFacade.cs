using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.UnitTests.Facade.Mocks
{
    public class MockInterfacedFacade : ITransactionFacade
    {
        private MockInterfacedFacade()
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
            private static MockInterfacedFacade _Instance = null;

            public static MockInterfacedFacade Build()
            {
                if (_Instance == null)
                {
                    _Instance = new MockInterfacedFacade();
                }

                return _Instance;
            }
        }
    }

      
}
