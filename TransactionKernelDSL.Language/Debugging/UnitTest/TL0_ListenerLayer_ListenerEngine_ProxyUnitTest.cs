
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
    public partial class TL0_ListenerLayer_ListenerEngine_ProxyUnitTest
	{           
                     [Test]
             public void Unit_Sale_OK_Test()
             {               
                try
                {
                    JsonParser testParser = new JsonParser("PDSXML",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_Sale_OK_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_Sale_OK_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_Sale_OK_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_Sale_OK_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_Sale_OK_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_Sale_OK_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_Sale_OK_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }
             }
             [Test]
             public void Unit_Sale_KO_Test()
             {               
                try
                {
                    JsonParser testParser = new JsonParser("PDSXML",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_Sale_KO_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_Sale_KO_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_Sale_KO_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_Sale_KO_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_Sale_KO_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_Sale_KO_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_Sale_KO_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }
             }
             [Test]
             public void Unit_BalanceQuery_OK_Test()
             {               
                try
                {
                    JsonParser testParser = new JsonParser("PDSXML",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_BalanceQuery_OK_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_BalanceQuery_OK_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_BalanceQuery_OK_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_BalanceQuery_OK_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_BalanceQuery_OK_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_BalanceQuery_OK_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_BalanceQuery_OK_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }
             }
             [Test]
             public void Unit_BalanceQuery_KO_Test()
             {               
                try
                {
                    JsonParser testParser = new JsonParser("PDSXML",false);

                    testParser.ResponseStructure = TL0_ListenerLayer_ListenerEngine_UnitTestInput.Unit_BalanceQuery_KO_Test();
                    if (testParser.AssembleMethod() == false) throw new AssertionException("Assemble failed");
                
                                                    #region Connection Set Up                               
                                 IPAddress testIpAddress; 
	 int testTcpPort; 
	 int testTimeout; 
	#region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out testTcpPort) == false) 
	testTcpPort = 0; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	if (IPAddress.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out testIpAddress) == false) 
	testIpAddress = IPAddress.Parse("127.0.0.1"); 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	if (Int32.TryParse((PDSNetFacade.Instance.GetValue("PDSNet.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out testTimeout) == false) 
	testTimeout = 30; 
	} 
	#endregion 
	#endregion 
	
                                #endregion

                                Debug.WriteLine("[Unit_BalanceQuery_KO_Test] Conectando a " + testIpAddress.ToString() + " - " + testTcpPort.ToString());
                                using (TcpClient testClient = new TcpClient(testIpAddress.ToString(), testTcpPort))
                                {
                                    testClient.ReceiveTimeout = testTimeout * 1000;
                                    Debug.WriteLine("[Unit_BalanceQuery_KO_Test] Sending...");                                  
                                    if (testParser.SendMethod((TcpClient)testClient) == false) throw new Exception("Error enviando datos de prueba");
                                    Debug.WriteLine("[Unit_BalanceQuery_KO_Test] Receiving...");
                                    if (testParser.ReceiveMethod((TcpClient)testClient) == false) throw new Exception("Error recibiendo datos de prueba");
                                    Debug.WriteLine("[Unit_BalanceQuery_KO_Test] Disassembling...");
                                    if (testParser.DisassembleMethod() == false) throw new Exception("Error desarmando datos de prueba");
                                    else
                                    {
                                        Thread.Sleep(200);
                                        if(TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_BalanceQuery_KO_Test(testParser.RequestStructure) == false)
                                        {
                                            throw new AssertionException("Test failed at TL0_ListenerLayer_ListenerEngine_UnitTestOutput.Unit_BalanceQuery_KO_Test");
                                        }
                                    }
                                }
                            }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception found: " + ex.Message);
                    Debug.WriteLine("SS: " + ex.StackTrace);
                    throw new AssertionException(ex.Message);
                }
             }
	}
}
#endif
	
    