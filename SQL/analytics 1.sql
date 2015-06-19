SELECT [Ticker], MaxPain, SUM([TradeValue]) AS 'Value'
  FROM [Markets].[dbo].[DailyTrades]
GROUP BY Ticker, MaxPain
  ORDER BY Ticker, [MaxPain] DESC 