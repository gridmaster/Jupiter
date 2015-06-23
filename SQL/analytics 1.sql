SELECT MaxPain, SUM([TradeValue]) AS 'Value'
  FROM [Markets].[dbo].[DailyTrades]
GROUP BY MaxPain
  ORDER BY [MaxPain] DESC 
  
  SELECT [Ticker], MaxPain, SUM([TradeValue]) AS 'Value'
  FROM [Markets].[dbo].[DailyTrades]
GROUP BY Ticker, MaxPain
  ORDER BY Ticker, [MaxPain] DESC 
  

SELECT c.Sector, MaxPain, SUM([TradeValue]) AS 'Value'
FROM [Markets].[dbo].[DailyTrades] d
JOIN [Markets].[dbo].[Companies] c on c.Symbol = d.Ticker
GROUP BY Sector, MaxPain
ORDER BY Value DESC -- Sector, [MaxPain] DESC 

SELECT c.Industry, MaxPain, SUM([TradeValue]) AS 'Value'
FROM [Markets].[dbo].[DailyTrades] d
JOIN [Markets].[dbo].[Companies] c on c.Symbol = d.Ticker
GROUP BY Industry, MaxPain
ORDER BY Value DESC

SELECT c.Name, c.Symbol, c.Industry, MaxPain, SUM([TradeValue]) AS 'Value'
FROM [Markets].[dbo].[DailyTrades] d
JOIN [Markets].[dbo].[Companies] c on c.Symbol = d.Ticker
WHERE c.Industry = 'Biotechnology'
GROUP BY Name, c.Symbol, industry, MaxPain
ORDER BY Value DESC