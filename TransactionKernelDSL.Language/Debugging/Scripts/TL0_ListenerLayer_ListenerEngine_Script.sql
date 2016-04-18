
                    USE []
                    GO
                 IF OBJECT_ID('CONFIGURACION') IS NULL 
	 BEGIN 
	 CREATE TABLE [dbo].[CONFIGURACION]( 
	 [cfg_instancia] [int] NOT NULL DEFAULT ((0)), 
	  [cfg_seccion] [varchar](100) NOT NULL, 
	  [cfg_clave] [varchar](100) NOT NULL, 
	  [cfg_numero] [int] NULL, 
	  [cfg_cadena] [varchar](1000) NULL, 
	  [cfg_descripcion] [varchar](255) NULL, 
	  CONSTRAINT [XPKCONFIGURACION] PRIMARY KEY CLUSTERED  
	  ( 
	  [cfg_instancia] ASC, 
	  [cfg_seccion] ASC, 
	  [cfg_clave] ASC 
	  ) 
	  WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] 
	  ) ON [PRIMARY] 
	  END 
	
                IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'ListenerEngine_ServerTCPPort' and cfg_instancia = 1) 
	BEGIN 
	INSERT INTO [CONFIGURACION] 
	  ([cfg_instancia] 
	  ,[cfg_seccion] 
	  ,[cfg_clave] 
	  ,[cfg_numero] 
	  ,[cfg_cadena] 
	  ,[cfg_descripcion]) 
	 VALUES 
	 (1
	 ,'CorrBanc.General'
	 ,'ListenerEngine_ServerTCPPort'
	 ,8888
	 ,NULL
	 ,'Server listenable Tcp Port')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'ListenerEngine_ServerIPAddress' and cfg_instancia = 1) 
	BEGIN 
	INSERT INTO [CONFIGURACION] 
	  ([cfg_instancia] 
	  ,[cfg_seccion] 
	  ,[cfg_clave] 
	  ,[cfg_numero] 
	  ,[cfg_cadena] 
	  ,[cfg_descripcion]) 
	 VALUES 
	 (1
	 ,'CorrBanc.General'
	 ,'ListenerEngine_ServerIPAddress'
	 ,NULL
	 ,'127.0.0.1'
	 ,'Server listenable Ip Address')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'ListenerEngineServerTimeoutSec' and cfg_instancia = 1) 
	BEGIN 
	INSERT INTO [CONFIGURACION] 
	  ([cfg_instancia] 
	  ,[cfg_seccion] 
	  ,[cfg_clave] 
	  ,[cfg_numero] 
	  ,[cfg_cadena] 
	  ,[cfg_descripcion]) 
	 VALUES 
	 (1
	 ,'CorrBanc.General'
	 ,'ListenerEngineServerTimeoutSec'
	 ,60
	 ,NULL
	 ,'Server Time Out in seconds')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'ListenerEngineMaxCompletionWorkThread' and cfg_instancia = 1) 
	BEGIN 
	INSERT INTO [CONFIGURACION] 
	  ([cfg_instancia] 
	  ,[cfg_seccion] 
	  ,[cfg_clave] 
	  ,[cfg_numero] 
	  ,[cfg_cadena] 
	  ,[cfg_descripcion]) 
	 VALUES 
	 (1
	 ,'CorrBanc.General'
	 ,'ListenerEngineMaxCompletionWorkThread'
	 ,1000
	 ,NULL
	 ,'Maximum completion work threads')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'ListenerEngineMinThread' and cfg_instancia = 1) 
	BEGIN 
	INSERT INTO [CONFIGURACION] 
	  ([cfg_instancia] 
	  ,[cfg_seccion] 
	  ,[cfg_clave] 
	  ,[cfg_numero] 
	  ,[cfg_cadena] 
	  ,[cfg_descripcion]) 
	 VALUES 
	 (1
	 ,'CorrBanc.General'
	 ,'ListenerEngineMinThread'
	 ,50
	 ,NULL
	 ,'Threads available in thread pool')
	END 
	                
                