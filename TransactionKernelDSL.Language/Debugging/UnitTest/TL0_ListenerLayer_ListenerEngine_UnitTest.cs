
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
	
using TransactionKernelDSL.Framework.Parser.Iso8583;            
#if DEBUG == true
namespace PDS.Switch.CorrBanc
{
    [TestFixture]
    public partial class TL0_ListenerLayer_ListenerEngine_UnitTest
	{
                     [Test]
             public void Unit_ConsultadeSaldo_OK_Test()
             {

                Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Starting engines...");
                 CorrBancFacade.Instance.StartEngines();
                Thread.Sleep(250);
                Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Engines started OK!");

                try
                {
                    Iso8583Parser testParser = new Iso8583Parser("ISO",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_ConsultadeSaldo_OK_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_OK_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_ConsultadeSaldo_OK_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_ConsultadeSaldo_OK_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    CorrBancFacade.Instance.StopEngines();

                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }


                CorrBancFacade.Instance.StopEngines();
             }
             [Test]
             public void Unit_ConsultadeSaldo_KO_Test()
             {

                Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Starting engines...");
                 CorrBancFacade.Instance.StartEngines();
                Thread.Sleep(250);
                Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Engines started OK!");

                try
                {
                    Iso8583Parser testParser = new Iso8583Parser("ISO",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_ConsultadeSaldo_KO_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((CorrBancFacade.Instance.GetValue("CorrBanc.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_ConsultadeSaldo_KO_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_ConsultadeSaldo_KO_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_ConsultadeSaldo_KO_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    CorrBancFacade.Instance.StopEngines();

                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }


                CorrBancFacade.Instance.StopEngines();
             }
	}
}
#endif
	
    