USE [Markets]
GO

/****** Object:  Table [dbo].[DailyStats]    Script Date: 06/17/2015 16:48:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TradeHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [nvarchar](40) NOT NULL,
	[BuyDate] [smalldatetime] NOT NULL,
	[BuyOpen] [money] NULL,
	[BuyHigh] [money] NULL,
	[BuyLow] [money] NULL,
	[BuyClose] [money] NULL,
	[SellDate] [smalldatetime] NULL,
	[SellOpen] [money] NULL,
	[SellHigh] [money] NULL,
	[SellLow] [money] NULL,
	[SellClose] [money] NULL,
	[TradeValue] [money] NULL
 CONSTRAINT [TradeHistory] PRIMARY KEY CLUSTERED 
(
	[Ticker] ASC,
	[BuyDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


