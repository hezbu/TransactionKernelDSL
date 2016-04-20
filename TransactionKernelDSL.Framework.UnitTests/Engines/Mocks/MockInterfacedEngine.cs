using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionKernelDSL.Framework.V2.Interfaces;

namespace TransactionKernelDSL.Framework.UnitTests.Engines.Mocks
{
    public class MockInterfacedEngine: ITransactionEngine
    {
        private MockInterfacedEngine()
        {
            _IsEngineOn = false;
        }

        private bool _IsEngineOn;

        public bool IsEngineOn
        {
            get { return _IsEngineOn; }
        }

        public bool Start()
        {
            _IsEngineOn = true;
            return true;
        }

        public bool Stop()
        {
            _IsEngineOn = false;
            return true;
        }

        public static class Factory
        {
            private static MockInterfacedEngine _Instance = null;

            public static MockInterfacedEngine Build()
            {
                if (_Instance == null)
                {
                    _Instance = new MockInterfacedEngine();
                }

                return _Instance;
            }
        }
    }
}
