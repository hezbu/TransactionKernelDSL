
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
	
        
        			IF (EXISTS (SELECT * FROM sysobjects where name='Sp_GetValue')) 
	BEGIN 
	DROP PROCEDURE Sp_GetValue 
	end 
	GO		
	 
	-- =============================================
	-- Author:		<TransactionKernelDSL>
	-- Create date: 17-4-2016
	-- Description:	<Obtiene un valor de una tabla de configuracion>
	-- =============================================
	CREATE PROCEDURE [Sp_GetValue]
	(
	    @cfg_instancia int = 0,
	    @cfg_seccion varchar(100),
	    @cfg_clave varchar(100) = NULL
	)
	AS
	
	IF @cfg_clave IS NULL
	BEGIN
	
	        SELECT cfg_numero, cfg_cadena
	        FROM CONFIGURACION with (nolock)
	        WHERE cfg_instancia = @cfg_instancia
	        AND cfg_seccion = @cfg_seccion
	END
	ELSE
	BEGIN
	
	        SELECT cfg_numero, cfg_cadena
	        FROM CONFIGURACION with (nolock)
	        WHERE cfg_instancia = @cfg_instancia
	        AND cfg_seccion = @cfg_seccion
	        AND cfg_clave = @cfg_clave
	
	
	END
	
	GO
	
        			IF OBJECT_ID('SequenceFactories') IS NULL 
	BEGIN 
	CREATE TABLE [SequenceFactories](
	[SequenceFactoryId] [int] IDENTITY(1,1) NOT NULL,
	[LastNumber] [int] NOT NULL,
	[InstanceId] [int] NOT NULL,
	[MaxNumber] [int] NOT NULL
	) ON [PRIMARY]
	END
	
	  GO
	      
	  IF (EXISTS (SELECT * FROM sysobjects where name='Sp_SequenceFactory'))
	BEGIN
	DROP PROCEDURE Sp_SequenceFactory
	end
	GO		
	
	-- =============================================
	-- Author:		<TransactionKernelDSL>
	-- Create date: 17-4-2016
	-- Description:	<Genera un secuenciador para cada componente del sistema>
	-- =============================================
	CREATE PROCEDURE [dbo].[Sp_SequenceFactory]
	( @instanceId		    int
	)
	AS
	BEGIN
	DECLARE @varNewNumber int, @varMaxNumber int
	
	            BEGIN TRANSACTION
	
	            UPDATE SequenceFactories
	            SET @varNewNumber = LastNumber = LastNumber + 1, @varMaxNumber = MaxNumber
	            WHERE instanceId = @instanceId
	
	            if(@varNewNumber > @varMaxNumber)
	            BEGIN
		            UPDATE SequenceFactories
		            SET @varNewNumber = LastNumber = 1
		            WHERE instanceId = @instanceId
	            END
	
	            SELECT @varNewNumber
	
	    COMMIT TRANSACTION
	
	END
	
	GO
	
	IF NOT EXISTS (SELECT * FROM SequenceFactories
	WHERE instanceId = 1)
	BEGIN
	        INSERT INTO [SequenceFactories]
		               ([LastNumber]
		               ,[InstanceId]
		               ,[MaxNumber])
	             VALUES
		               (1
		               ,1
		               ,999999)
	END
	
	GO
	
			
        		IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'TelnetLoggerOn' and cfg_instancia = 1) 
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
	 ,'TelnetLoggerOn'
	 ,NULL
	 ,'False'
	 ,'Enables a telnet logger on port 23101')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'LogDirectory' and cfg_instancia = 1) 
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
	 ,'LogDirectory'
	 ,NULL
	 ,'C:\Logs\CorrBanc'
	 ,'Directory where logger will be writing to')
	END 
	IF NOT EXISTS (SELECT cfg_instancia FROM CONFIGURACION WHERE cfg_seccion = 'CorrBanc.General' and cfg_clave = 'LogPrefix' and cfg_instancia = 1) 
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
	 ,'LogPrefix'
	 ,NULL
	 ,'CorrBancMainLogger'
	 ,'Prefix of MainLogger files')
	END 
	
        