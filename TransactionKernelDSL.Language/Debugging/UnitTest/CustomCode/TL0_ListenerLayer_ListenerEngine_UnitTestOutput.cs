
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using System.Diagnostics;    
using TransactionKernelDSL.Framework.Parser.Json;            
#if DEBUG == true
namespace PDS.Switch.PDSNet
{
    
    public partial class TL0_ListenerLayer_ListenerEngine_UnitTestOutput
	{
                    
                public static bool Unit_Sale_OK_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
            
                public static bool Unit_Sale_KO_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
            
                public static bool Unit_BalanceQuery_OK_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
            
                public static bool Unit_BalanceQuery_KO_Test(AbstractTransactionParserStructure structure)
                {
                    return true;
                }
	}
}
#endif
	
    