USE [SANTA]
GO

/****** Object:  Table [dbo].[usuarios]    Script Date: 5/1/2020 7:05:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[usuarios](
	[usuclave] [varchar](20) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[aPaterno] [varchar](30) NOT NULL,
	[aMaterno] [varchar](30) NULL,
	[pass] [varbinary](max) NULL,
	[fechaAlta] [datetime] NOT NULL,
	[status] [int] NOT NULL,
	[telefono] [varchar](15) NULL,
	[fechaUltAct] [datetime] NOT NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[usuclave] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[usuarios] ADD  CONSTRAINT [DF_usuarios_fechaAlta]  DEFAULT (getdate()) FOR [fechaAlta]
GO

ALTER TABLE [dbo].[usuarios] ADD  CONSTRAINT [DF_usuarios_status]  DEFAULT ((1)) FOR [status]
GO

ALTER TABLE [dbo].[usuarios] ADD  CONSTRAINT [DF_usuarios_fechaUltAct]  DEFAULT (getdate()) FOR [fechaUltAct]
GO


