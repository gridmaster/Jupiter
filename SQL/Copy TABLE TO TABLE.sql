CREATE TABLE #TmpTable( [Id] [int] IDENTITY(1,1) NOT NULL, [Date] [datetime] NOT NULL, 
                            [EtfName] [nvarchar](200) NOT NULL, [ExchangeId] int NULL, [Exchange] [nvarchar](60) NULL,  
                             [Ticker] [nvarchar](30) NOT NULL)

SELECT * FROM #TmpTable
SELECT * FROM ETFBase

INSERT #TmpTable ( [Date], [EtfName], [Ticker] )
SELECT [Date], [EtfName], [Ticker]
  FROM [tickersymbols].[dbo].[ETFTradingVolumes]
    WHERE Date > '6/13/2015' 

UPDATE ETFBase SET [ExchangeId] = t.ExchangeId, [Exchange] = t.[Exchange]

SELECT t.*
FROM ETFBase e
INNER JOIN #TmpTable t ON t.EtfName = e.EtfName

DROP TABLE #TmpTable;

-- UPDATE Sales.SalesPerson
-- SET Bonus = 6000, CommissionPct = .10, SalesQuota = NULL;

UPDATE [Markets].[dbo].[Industries]
SET SectorId = i.Id
FROM [Markets].[dbo].[Industries] i
JOIN [Markets].[dbo].[Sectors] s on s.Name = i.Sector AND s.Date = i.Date
WHERE i.SectorId = 0