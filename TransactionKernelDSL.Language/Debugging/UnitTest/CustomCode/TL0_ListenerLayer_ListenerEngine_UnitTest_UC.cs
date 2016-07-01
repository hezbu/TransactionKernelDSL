
	using System;
	using System.Collections.Generic;
	using System.Text;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Threading;
    using System.Diagnostics;
    using NUnit.Framework;
using System.Net.Sockets; 
	 using System.Net; 
	
using TransactionKernelDSL.Framework.Parser.Json;            
#if DEBUG == true
namespace PDS.Switch.PDSNet
{
    [TestFixture]
    public partial class TL0_ListenerLayer_ListenerEngine_UnitTest
	{
            [TestFixtureSetUp]
            public void ListenerEngine_Setup()
            {
                
            }

            [TestFixtureTearDown]
            public void ListenerEngine_TearDown()
            {
               
            }      
	}
}
#endif
	
    