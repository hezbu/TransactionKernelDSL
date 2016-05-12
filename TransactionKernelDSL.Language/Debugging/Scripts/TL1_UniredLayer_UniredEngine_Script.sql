
                    USE [1]
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
	
                
                                  