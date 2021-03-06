/****** Script for SelectTopNRows command from SSMS  ******/
--SELECT MAX([Date]) FROM [Markets].[dbo].[Companies]

SELECT 
       [Symbol]
      ,[Exchange], 'company' AS EntityType
  FROM [Markets].[dbo].[Companies]
  WHERE (Exchange LIKE 'NYSE%' OR Exchange LIKE 'Nasd%')
  AND Date = (SELECT MAX([Date]) FROM [Markets].[dbo].[Companies])
  UNION
  SELECT [Ticker] AS Symbol
      ,[Exchange], 'etf' AS EntityType
  FROM [Markets].[dbo].[ETFs]
    WHERE (Exchange LIKE 'NYSE%' OR Exchange LIKE 'Nasd%')
  AND Date = (SELECT MAX([Date]) FROM [Markets].[dbo].[ETFs])
 ORDER BY Symbol