USE [SANTA]
GO

/****** Object:  Table [dbo].[clientes]    Script Date: 5/1/2020 7:12:47 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[clientes](
	[numcliente] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[aPaterno] [varchar](30) NOT NULL,
	[aMaterno] [varchar](30) NULL,
	[calle] [varchar](100) NOT NULL,
	[numeroExt] [int] NOT NULL,
	[numeroInt] [int] NULL,
	[colonia] [varchar](100) NOT NULL,
	[telefono] [varchar](15) NOT NULL,
	[usuclaveUltAct] [varchar](20) NOT NULL,
 CONSTRAINT [PK_clientes] PRIMARY KEY CLUSTERED 
(
	[numcliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK_clientes_usuarios] FOREIGN KEY([usuclaveUltAct])
REFERENCES [dbo].[usuarios] ([usuclave])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK_clientes_usuarios]
GO


