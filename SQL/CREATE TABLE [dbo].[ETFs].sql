USE [Markets]
GO

/****** Object:  Table [dbo].[ETFs]    Script Date: 06/18/2015 09:34:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ETFs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[EtfName] [nvarchar](200) NOT NULL,
	[ExchangeId] [int] NULL,
	[Exchange] [nvarchar](60) NULL,
	[Ticker] [nvarchar](30) NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.ETFs] PRIMARY KEY CLUSTERED 
(
	[Date] ASC,
	[EtfName] ASC,
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


