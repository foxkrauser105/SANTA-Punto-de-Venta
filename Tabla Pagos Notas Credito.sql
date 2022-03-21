USE [SANTA]
GO

/****** Object:  Table [dbo].[pagos_notas_credito]    Script Date: 8/23/2020 6:41:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pagos_notas_credito](
	[numcliente] [int] NOT NULL,
	[ncfolio] [int] NOT NULL,
	[pago] [int] NOT NULL,
	[importe] [decimal](7, 2) NOT NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK__pagos_no__41729FBCBBD3CD1E] PRIMARY KEY CLUSTERED 
(
	[numcliente] ASC,
	[ncfolio] ASC,
	[pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[pagos_notas_credito] ADD  CONSTRAINT [DF_pagos_notas_credito_fecha]  DEFAULT (getdate()) FOR [fecha]
GO


