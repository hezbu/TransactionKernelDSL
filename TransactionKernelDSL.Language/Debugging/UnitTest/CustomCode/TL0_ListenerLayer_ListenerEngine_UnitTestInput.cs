
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
    
    public partial class TL0_ListenerLayer_ListenerEngine_UnitTestInput
	{
                    
                public static JsonStructure Unit_Sale_OK_Test()
                {
                   
				    JsonStructure testStructure = new JsonStructure(AbstractTransactionParserStructureType.Request,"PDSXML");
                    throw new NotImplementedException(" Unit_Sale_OK_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
            
                public static JsonStructure Unit_Sale_KO_Test()
                {
                   
				    JsonStructure testStructure = new JsonStructure(AbstractTransactionParserStructureType.Request,"PDSXML");
                    throw new NotImplementedException(" Unit_Sale_KO_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
            
                public static JsonStructure Unit_BalanceQuery_OK_Test()
                {
                   
				    JsonStructure testStructure = new JsonStructure(AbstractTransactionParserStructureType.Request,"PDSXML");
                    throw new NotImplementedException(" Unit_BalanceQuery_OK_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
            
                public static JsonStructure Unit_BalanceQuery_KO_Test()
                {
                   
				    JsonStructure testStructure = new JsonStructure(AbstractTransactionParserStructureType.Request,"PDSXML");
                    throw new NotImplementedException(" Unit_BalanceQuery_KO_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
	}
}
#endif
	
    