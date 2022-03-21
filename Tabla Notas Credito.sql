USE [SANTA]
GO

/****** Object:  Table [dbo].[notas_credito]    Script Date: 5/1/2020 7:04:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[notas_credito](
	[numcliente] [int] NOT NULL,
	[ncfolio] [int] NOT NULL,
	[status] [varchar](2) NOT NULL,
	[fechaAlta] [datetime] NOT NULL,
	[fechaCompromiso] [date] NOT NULL,
	[monto] [decimal](7, 2) NOT NULL,
	[montoPagado] [decimal](7, 2) NOT NULL,
 CONSTRAINT [PK__notas_cr__38F1A0E355A198F5] PRIMARY KEY CLUSTERED 
(
	[numcliente] ASC,
	[ncfolio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[notas_credito] ADD  CONSTRAINT [DF_notas_credito_montoPagado]  DEFAULT ((0)) FOR [montoPagado]
GO


