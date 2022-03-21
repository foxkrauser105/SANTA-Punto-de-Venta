USE [SANTA]
GO

/****** Object:  Table [dbo].[registro_notas_credito]    Script Date: 5/1/2020 7:05:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[registro_notas_credito](
	[numcliente] [int] NOT NULL,
	[ncfolio] [int] NOT NULL,
	[detalle] [int] NOT NULL,
	[id_producto] [varchar](20) NOT NULL,
	[cantidad] [float] NOT NULL,
	[precio] [decimal](7, 2) NOT NULL,
	[importe] [decimal](7, 2) NOT NULL,
	[fechaSurtido] [datetime] NOT NULL,
	[descuento] [int] NOT NULL,
 CONSTRAINT [PK__registro__3363B4A14AFE0FD6] PRIMARY KEY CLUSTERED 
(
	[numcliente] ASC,
	[ncfolio] ASC,
	[detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[registro_notas_credito] ADD  CONSTRAINT [DF_registro_notas_credito_fechaSurtido]  DEFAULT (getdate()) FOR [fechaSurtido]
GO

ALTER TABLE [dbo].[registro_notas_credito]  WITH CHECK ADD  CONSTRAINT [FK_registro_notas_credito_productos] FOREIGN KEY([id_producto])
REFERENCES [dbo].[productos] ([id_producto])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[registro_notas_credito] CHECK CONSTRAINT [FK_registro_notas_credito_productos]
GO


