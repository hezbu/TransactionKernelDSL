using System;
using System.Collections.Generic;
using System.Text;
using TransactionKernelDSL.Framework.V1;

namespace TransactionKernelDSL.Framework.Test
{
    public class TestEngine : AbstractTcpTriggeredMultiThreadedInputTransactionEngine
    {

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        #region Static fields
        protected static TestEngine _Instance = null;
        #endregion

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        public static TestEngine Instance
        {
            get 
            {
                if (_Instance == null)
                {
                    _Instance = new TestEngine();
                }

                return _Instance;
            }
        }

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        private TestEngine()
        {

        }
           

        //protected override AbstractTransactionNexus NexusFactory()
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// OVERRIDE y TRANSFORMACION CON LOS SWITCH CASES DEFINIDOS
        /// </summary>
        protected override AbstractTransactionHandler TransactionHandlerFactory(object transactionId)
        {
            throw new NotImplementedException();
        }



        protected override AbstractTransactionParser ParserFactory(object state = null)
        {
            throw new NotImplementedException();
        }
    }
}
