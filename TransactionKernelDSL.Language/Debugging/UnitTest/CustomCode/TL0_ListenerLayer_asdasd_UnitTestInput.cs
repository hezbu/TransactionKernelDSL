
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
    
    public partial class TL0_ListenerLayer_asdasd_UnitTestInput
	{
                    
                public static Iso8583Structure Unit_TransactionName1_OK_Test()
                {
                   
				    Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Request,"1");
                    throw new NotImplementedException(" Unit_TransactionName1_OK_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
            
                public static Iso8583Structure Unit_TransactionName1_KO_Test()
                {
                   
				    Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Request,"1");
                    throw new NotImplementedException(" Unit_TransactionName1_KO_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
	}
}
#endif
