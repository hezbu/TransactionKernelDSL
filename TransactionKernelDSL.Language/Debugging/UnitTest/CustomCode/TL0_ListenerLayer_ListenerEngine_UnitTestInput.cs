
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
namespace PDS.Switch.CorrBanc
{
    
    public partial class TL0_ListenerLayer_ListenerEngine_UnitTestInput
	{
                    
                public static Iso8583Structure Unit_ConsultadeSaldo_OK_Test()
                {
                    Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Request,"ISO");
                    throw new NotImplementedException(" Unit_ConsultadeSaldo_OK_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
            
                public static Iso8583Structure Unit_ConsultadeSaldo_KO_Test()
                {
                    Iso8583Structure testStructure = new Iso8583Structure(AbstractTransactionParserStructureType.Request,"ISO");
                    throw new NotImplementedException(" Unit_ConsultadeSaldo_KO_Test() Must be implemented in order to generate a valid input regarding to the references unit test");
                    return testStructure;
                }
	}
}
#endif
