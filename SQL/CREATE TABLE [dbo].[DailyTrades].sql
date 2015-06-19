USE [Markets]
GO

/****** Object:  Table [dbo].[DailyTrades_20150618]    Script Date: 06/18/2015 17:38:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DailyTrades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [nvarchar](40) NOT NULL,
	[BuyDate] [smalldatetime] NOT NULL,
	[MaxPain] [money] NOT NULL,
	[BuyOpen] [money] NULL,
	[BuyHigh] [money] NULL,
	[BuyLow] [money] NULL,
	[BuyClose] [money] NULL,
	[BuyVolume] [decimal](18, 0) NULL,
	[SellDate] [smalldatetime] NULL,
	[SellOpen] [money] NULL,
	[SellHigh] [money] NULL,
	[SellLow] [money] NULL,
	[SellClose] [money] NULL,
	[SellVolume] [decimal](18, 0) NULL,
	[TradeValue] [money] NULL,
	[CurrentLow] [money] NULL,
 CONSTRAINT [PK_dbo.DailyTrade] PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC,
	[BuyDate] ASC,
	[MaxPain] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


