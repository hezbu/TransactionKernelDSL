
    using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Configuration;
    using System.Xml;
    using TransactionKernelDSL.Framework.V1;
    using log4net.Config;
    using log4net;
    using System.Net;
using System.Reflection;
    using System.Threading;
    using System.Data.SqlClient;
    using System.Data;
	
namespace PDS.Switch.PDSNet
{

    /// <summary>
    /// The transactional facade for solution PDSNet
    /// </summary>
    /// <remarks>
    /// Generated on 29/06/2016 11:40:08
    /// </remarks>
	public partial class PDSNetFacade : AbstractTransactionFacade
	{
        internal const string ConstStan = "ConstStan";

        private static PDSNetFacade _Instance = null;


              private string _GetValueStoredProc = null;
              internal string GetValueStoredProc { get { return _GetValueStoredProc; } }
              private string _SequenceFactoryStoredProc = null;
              internal string SequenceFactoryStoredProc { get { return _SequenceFactoryStoredProc; } }
       
#if DEBUG == true
                public DebugTransactionStatus TL0_ListenerLayer_ListenerEngine_Engine_Status
                {
                    get
                    {
                        return TL0_ListenerLayer_ListenerEngine_Engine.Instance.DebugStatus;
                    }
                }
                            public DebugTransactionStatus TL0_ListenerLayer_asdasd_Engine_Status
                {
                    get
                    {
                        return TL0_ListenerLayer_asdasd_Engine.Instance.DebugStatus;
                    }
                }
                            public DebugTransactionStatus TL1_UniredLayer_UniredEngine_Engine_Status
                {
                    get
                    {
                        return TL1_UniredLayer_UniredEngine_Engine.Instance.DebugStatus;
                    }
                }
                            public DebugTransactionStatus TL1_RedIn_RedinEngine_Engine_Status
                {
                    get
                    {
                        return TL1_RedIn_RedinEngine_Engine.Instance.DebugStatus;
                    }
                }
            #endif

        /// <summary>
        /// TRANSFORMACION
        /// </summary>
        public static PDSNetFacade Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PDSNetFacade();
                }

                return _Instance as PDSNetFacade;
            }
        }


         /// <summary>
        /// TRANSFORMACION - Argumento MANUAL
        /// </summary>
        private PDSNetFacade()
        {
            int instanceId = 0;
          
            this._ConnectionString = ConfigurationManager.ConnectionStrings["MainConnection"].ConnectionString;

            _GetValueStoredProc = ConfigurationManager.AppSettings["GetValueStoredProcedure"];
            _SequenceFactoryStoredProc = ConfigurationManager.AppSettings["SequenceFactoryStoredProcedure"];
       

            if (Int32.TryParse(ConfigurationManager.AppSettings["InstanceId"], out instanceId) == false) throw new ApplicationException("InstanceId is not a number (" + ConfigurationManager.AppSettings["InstanceId"] + ")");

            this._InstanceId = instanceId;
            List<string> results = new List<string>();

            #region Facade Setup
            #region Telnet Logger
            _IsTelnetLoggerOn = Convert.ToBoolean((GetValue("PDSNet.General", "TelnetLoggerOn") as List<string>)[0]);
            _TelnetLogPort = 23100 + Convert.ToInt32(this._InstanceId);
            #endregion
            #region Logger
            this._LogDirectory = (GetValue("PDSNet.General", "LogDirectory") as List<string>)[0];
            this._LogPrefix = (GetValue("PDSNet.General", "LogPrefix") as List<string>)[0];
            #endregion
            #region Log4Net Setup
            XmlDocument log4NetConfiguration = new XmlDocument();
            log4NetConfiguration.LoadXml("<log4net debug=\"false\">" +
                                ((_IsTelnetLoggerOn == false) ? String.Empty : "<appender name=\"HomeroTelnetLogger\" type=\"log4net.Appender.TelnetAppender\"><port value=\"" + _TelnetLogPort.ToString() + "\" /><layout type=\"log4net.Layout.PatternLayout\"><conversionPattern value=\"%date{HH:mm:ss.fff, }|Thread ID: %thread|%level |%method|%m%n\" /></layout></appender>") +
                                ((Environment.UserInteractive == false) ? String.Empty : "<appender name=\"HomeroColoredConsoleAppender\" type=\"log4net.Appender.ColoredConsoleAppender\"><mapping><level value=\"ERROR\" /><foreColor value=\"White\" /><backColor value=\"Red, HighIntensity\" /></mapping><mapping><level value=\"FATAL\" /><foreColor value=\"Red, HighIntensity\" /></mapping><layout type=\"log4net.Layout.PatternLayout\"><conversionPattern value=\"[%thread]%m%n\" /></layout></appender>") +
                                "<appender name=\"HomeroLogger\" type=\"log4net.Appender.RollingFileAppender\"><PreserveLogFileNameExtension value=\"true\" /><file value=\" " + _LogDirectory + "\\" + _LogPrefix + ".log \" /><appendToFile value=\"true\" /><rollingStyle value=\"Date\" /><datePattern value=\"'.'yyyyMMdd\" /><layout type=\"log4net.Layout.PatternLayout\"><conversionPattern value=\"%date{HH:mm:ss.fff, }|Thread ID: %thread|%level |%method|%m%n\" /></layout></appender>" +
                                "<appender name=\"HomeroLoggerDebug\" type=\"log4net.Appender.DebugAppender\" ><layout type=\"log4net.Layout.PatternLayout\"><conversionPattern value=\"" + "%date{HH:mm:ss.fff, }|Thread ID: %thread|%level |%method|%m%n" + "\" /></layout></appender>" +
                                        
                                "<logger name=\"MainLogger\"><level value=\"ALL\" /><appender-ref ref=\"HomeroLogger\" />" +
                                 ((_IsTelnetLoggerOn == false) ? String.Empty : "<appender-ref ref=\"HomeroTelnetLogger\" />") +
                                 ((Environment.UserInteractive == false) ? String.Empty : "<appender-ref ref=\"HomeroColoredConsoleAppender\" />") +
                                 "<appender-ref ref=\"HomeroLoggerDebug\" />" +
                                "</logger>" +
                                "</log4net>");
            XmlConfigurator.Configure(log4NetConfiguration.DocumentElement);
            _Log = LogManager.GetLogger("MainLogger");
            #endregion
            #endregion
            #region Engines Setup
            #region Listener Engine Setup 
TL0_ListenerLayer_ListenerEngine_Engine.Instance.Logger = "MainLogger";
	#region Listener Engine Server Tcp Port 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "ListenerEngine_ServerTCPPort") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 0; 
	TL0_ListenerLayer_ListenerEngine_Engine.Instance.ListenerTcpPort = intDefaultValue; 
	} 
	#endregion 
	#region Listener Engine Server Ip Address 
{ 
	 IPAddress defaultAddress = IPAddress.Parse("127.0.0.1");
	if (IPAddress.TryParse((GetValue("PDSNet.General", "ListenerEngine_ServerIPAddress") as List<string>)[0], out defaultAddress) == false) 
	defaultAddress = IPAddress.Parse("127.0.0.1"); 
	TL0_ListenerLayer_ListenerEngine_Engine.Instance.ListenerIpAddress = defaultAddress; 
	} 
	#endregion 
	#region Listener Engine Server Timeout Setup 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "ListenerEngineServerTimeoutSec") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 30; 
	TL0_ListenerLayer_ListenerEngine_Engine.Instance.ListenerTimeout = intDefaultValue; 
	} 
	#endregion 
	#region Listener Engine Min Thread 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "ListenerEngineMinThread") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 250; 
	TL0_ListenerLayer_ListenerEngine_Engine.Instance.ListenerMinThreads = intDefaultValue; 
	} 
	#endregion 
	#region Listener Engine Completion Work Threads 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "ListenerEngineMaxCompletionWorkThread") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 1000; 
	TL0_ListenerLayer_ListenerEngine_Engine.Instance.ListenerMinCompletionWorkThreads = intDefaultValue; 
	} 
	#endregion 
	#endregion 
	
#region asdasd Setup 
TL0_ListenerLayer_asdasd_Engine.Instance.Logger = "MainLogger";
	#region asdasd Server Tcp Port 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "asdasd_ServerTCPPort") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 0; 
	TL0_ListenerLayer_asdasd_Engine.Instance.ListenerTcpPort = intDefaultValue; 
	} 
	#endregion 
	#region asdasd Server Ip Address 
{ 
	 IPAddress defaultAddress = IPAddress.Parse("127.0.0.1");
	if (IPAddress.TryParse((GetValue("PDSNet.General", "asdasd_ServerIPAddress") as List<string>)[0], out defaultAddress) == false) 
	defaultAddress = IPAddress.Parse("127.0.0.1"); 
	TL0_ListenerLayer_asdasd_Engine.Instance.ListenerIpAddress = defaultAddress; 
	} 
	#endregion 
	#region asdasd Server Timeout Setup 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "asdasdServerTimeoutSec") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 30; 
	TL0_ListenerLayer_asdasd_Engine.Instance.ListenerTimeout = intDefaultValue; 
	} 
	#endregion 
	#region asdasd Min Thread 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "asdasdMinThread") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 250; 
	TL0_ListenerLayer_asdasd_Engine.Instance.ListenerMinThreads = intDefaultValue; 
	} 
	#endregion 
	#region asdasd Completion Work Threads 
{ 
	int intDefaultValue = 0;
	if (Int32.TryParse((GetValue("PDSNet.General", "asdasdMaxCompletionWorkThread") as List<string>)[0], out intDefaultValue) == false) 
	intDefaultValue = 1000; 
	TL0_ListenerLayer_asdasd_Engine.Instance.ListenerMinCompletionWorkThreads = intDefaultValue; 
	} 
	#endregion 
	#endregion 
	
#region Unired Engine Setup 
TL1_UniredLayer_UniredEngine_Engine.Instance.Logger = "MainLogger";
	TL1_UniredLayer_UniredEngine_Engine.Instance.EngineLogger = "MainLogger";
	#endregion 
	
#region Redin Engine Setup 
TL1_RedIn_RedinEngine_Engine.Instance.Logger = "MainLogger";
	TL1_RedIn_RedinEngine_Engine.Instance.EngineLogger = "MainLogger";
	#endregion 
	
            #endregion            
#region Web Services Setup 
	#endregion
	
            #region Environment Variables Setup
            
            #endregion
            #region User Customized Setup
            PDSNetFacadeUserCustomizedSetup();
            #endregion

        }

      
                  public override object GetValue(string section, string key = null)
            {
            List<string> results = new List<string>();
            try            
            {
            #region Using SQL Connection
            using (SqlConnection objSQLConnection = new SqlConnection())
            {
                objSQLConnection.ConnectionString = this._ConnectionString;
                objSQLConnection.Open();

                #region Using Sql Command
                using (SqlCommand objSQLCommand = new SqlCommand())
                {
                    objSQLCommand.CommandType = CommandType.StoredProcedure;
                    objSQLCommand.CommandText = _GetValueStoredProc;

                    objSQLCommand.Parameters.Add(new SqlParameter("@cfg_instancia", SqlDbType.Int)).Value = this._InstanceId;
                    objSQLCommand.Parameters.Add(new SqlParameter("@cfg_seccion", SqlDbType.VarChar, 100)).Value = section;
                    if (key != null) objSQLCommand.Parameters.Add(new SqlParameter("@cfg_clave", SqlDbType.VarChar, 100)).Value = key;


                    objSQLCommand.Connection = objSQLConnection;

                    #region Using SqlDataReader
                    using (SqlDataReader objSQLReader = objSQLCommand.ExecuteReader())
                    {

                        if (objSQLReader.HasRows == true)
                        {
                            while (objSQLReader.Read())
                            {
                                if (!String.IsNullOrEmpty(objSQLReader[0].ToString())) results.Add(objSQLReader[0].ToString());
                                else if (!String.IsNullOrEmpty(objSQLReader[1].ToString())) results.Add(objSQLReader[1].ToString());

                            }
                        }
                    }
                    #endregion
                }
                #endregion
                objSQLConnection.Close();
            }
            #endregion
            }
            catch(Exception ex)
            {
                _Log.Fatal("Exception found in GetValue(): "+ex.Message);
                _Log.Fatal("StackTrace is " + ex.StackTrace);
            }
            
        return results;
        }
  public override object SequenceFactory()
        {
            string result = null;
            try            
            {
            #region Using SQL Connection
            using (SqlConnection objSQLConnection = new SqlConnection())
            {
                objSQLConnection.ConnectionString = this._ConnectionString;
                objSQLConnection.Open();

                #region Using Sql Command
                using (SqlCommand objSQLCommand = new SqlCommand())
                {
                    objSQLCommand.CommandType = CommandType.StoredProcedure;
                    objSQLCommand.CommandText = _SequenceFactoryStoredProc;

                    objSQLCommand.Parameters.Add(new SqlParameter("@instanceId", SqlDbType.Int)).Value = this._InstanceId;
                    objSQLCommand.Connection = objSQLConnection;

                    #region Using SqlDataReader
                    using (SqlDataReader objSQLReader = objSQLCommand.ExecuteReader())
                    {

                        if (objSQLReader.HasRows == true)
                        {
                            if (objSQLReader.Read())
                            {
                                if (!String.IsNullOrEmpty(objSQLReader[0].ToString()))
                                    result = objSQLReader[0].ToString();
                            }
                        }
                    }
                    #endregion
                }
                #endregion
                objSQLConnection.Close();
            }
            #endregion
            }
            catch(Exception ex)
            {
                _Log.Fatal("Exception found in SequenceFactory(): "+ex.Message);
                _Log.Fatal("StackTrace is " + ex.StackTrace);
            }
            return result;
      }
  

        /// <summary>
        /// TRANSFORMACION - El orden de prendido es de niveles mas altos a mas bajos (desde TLn a TLx donde n > x)
        ///                  Debe llenarse con los engines que se definan en el DSL
        /// </summary>        
        public override bool StartEngines()
        {

            #region Start Log
            _Log.Info("## Service Launch PDSNet - Instance Id = " + _InstanceId + " ##");
            #region Loaded Assemblies
            foreach (Assembly currentAssembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (currentAssembly.GetName().FullName.Contains("TransactionKernelDSL") == true)
                {
                    _Log.Info("Loaded: [" + currentAssembly.GetName().FullName + "]");
                }
            }
            #endregion
            #endregion

   if ( TL1_UniredLayer_UniredEngine_Engine.Instance.Start() == false)
            {
                _Log.Fatal("Cannot start engine  TL1_UniredLayer_UniredEngine_Engine");
                return false;
            }    
      if ( TL1_RedIn_RedinEngine_Engine.Instance.Start() == false)
            {
                _Log.Fatal("Cannot start engine  TL1_RedIn_RedinEngine_Engine");
                return false;
            }    
      if ( TL0_ListenerLayer_ListenerEngine_Engine.Instance.Start() == false)
            {
                _Log.Fatal("Cannot start engine  TL0_ListenerLayer_ListenerEngine_Engine");
                return false;
            }    
      if ( TL0_ListenerLayer_asdasd_Engine.Instance.Start() == false)
            {
                _Log.Fatal("Cannot start engine  TL0_ListenerLayer_asdasd_Engine");
                return false;
            }    
   
            #region User Customized Start Engines Method
            PDSNetFacadeStartEngines_UC();
            #endregion

            return true;
        }

         /// <summary>
        /// TRANSFORMACION - El orden de apagado es de niveles mas bajos a mas altos (desde TLx a TLn donde x < n)
        ///                - Debe llenarse con los engines que se definan en el DSL
        /// </summary>  
        public override bool StopEngines()
        {
   if ( TL0_ListenerLayer_ListenerEngine_Engine.Instance.Stop() == false)
            {
                _Log.Fatal("Cannot stop engine  TL0_ListenerLayer_ListenerEngine_Engine");
                return false;
            }    
      if ( TL0_ListenerLayer_asdasd_Engine.Instance.Stop() == false)
            {
                _Log.Fatal("Cannot stop engine  TL0_ListenerLayer_asdasd_Engine");
                return false;
            }    
   
   if ( TL1_UniredLayer_UniredEngine_Engine.Instance.Stop() == false)
            {
                _Log.Fatal("Cannot stop engine  TL1_UniredLayer_UniredEngine_Engine");
                return false;
            }    
      if ( TL1_RedIn_RedinEngine_Engine.Instance.Stop() == false)
            {
                _Log.Fatal("Cannot stop engine  TL1_RedIn_RedinEngine_Engine");
                return false;
            }    
   
            #region User Customized Stop Engines Method
            PDSNetFacadeStopEngines_UC();
            #endregion
            return true;
        }


    }
}
