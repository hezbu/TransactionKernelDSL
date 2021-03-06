
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
	
namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// A transactional engine named Redin Engine
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:19
    /// </remarks>
public partial class TL1_RedIn_RedinEngine_Engine: AbstractOutputTransactionEngine
	{
         

                            public override TransmissionStatus Resolve(AbstractTransactionParser parser)
                            {
                                throw new NotImplementedException("Resolve() must be implemented as the method that sets a requirement and eventually gets a response");
                            }

                            public override bool IsEngineOn
                            {
                                get { throw new NotImplementedException("Resolve() must be implemented in order to know from other classes whether this engine is working or not"); }
                            }

                            public override bool Start()
                            {
                                throw new NotImplementedException("Start() must be implemented in order to start the engine");
                            }

                            public override bool Stop()
                            {
                                throw new NotImplementedException("Stop() must be implemented in order to stop the engine");
                            }

                         	}
}
