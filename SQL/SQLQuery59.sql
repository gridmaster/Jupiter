/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [Id]
      ,[Date]
      ,[Name]
      ,[Symbol]
      ,[ExchangeId]
      ,[Exchange]
      ,[IndustryId]
      ,[Industry]
      ,[SectorId]
      ,[Sector]
      ,[URI]
      ,[OneDayPriceChangePercent]
      ,[MarketCap]
      ,[PriceToEarnings]
      ,[ROEPercent]
      ,[DivYieldPercent]
      ,[LongTermDebtToEquity]
      ,[PriceToBookValue]
      ,[NetProfitMarginPercentMRQ]
      ,[PriceToFreeCashFlowMRQ]
      ,[GeneralInfoURI]
  FROM [Markets].[dbo].[Companies]