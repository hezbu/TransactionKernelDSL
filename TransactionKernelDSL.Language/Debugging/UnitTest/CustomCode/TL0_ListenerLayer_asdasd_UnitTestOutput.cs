
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using System.Diagnostics;    
using TransactionKernelDSL.Framework.Parser.Iso8583;            
#if DEBUG == true
namespace PDS.Switch.PDSNet
{
    
    public partial class TL0_ListenerLayer_asdasd_UnitTestOutput
	{
                    
                public static bool Unit_TransactionName1_OK_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
            
                public static bool Unit_TransactionName1_KO_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
	}
}
#endif
