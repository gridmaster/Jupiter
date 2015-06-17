using System.Collections.Generic;
using System.Data;
using Jupiter.Models;
using Jupiter.Models.BulkLoad;

namespace Jupiter.BulkLoad
{
    public class BulkLoadSector : BaseBulkLoad
    {
        private static readonly string[] ColumnNames = new string[] { "Date", "DebtToEquity", "DivYieldPerCent", 
            "MarketCap", "Name", "NetProfitMarginMrq", "OneDayPriceChgPerCent", "PriceToBook", "PriceToEarnings", "PriceToFreeCashFlowMrq", "ROEPerCent" };

        public BulkLoadSector() : base(ColumnNames)
        {
            
        }

        //public DataTable LoadDataTableWithSectors(IEnumerable<Sector> dStats, DataTable dt)
        //{
        //    foreach (var value in dStats)
        //    {
        //        var sValue = value.Date + "^" + value.DebtToEquity + "^"
        //                     + value.DivYieldPerCent + "^" + value.MarketCap
        //                     + "^" + value.Name + "^" + value.NetProfitMarginMrq + "^" + value.OneDayPriceChgPerCent
        //                     + "^" + value.PriceToBook + "^" + value.PriceToEarnings + "^" + value.PriceToFreeCashFlowMrq + "^" + value.ROEPerCent;

        //        DataRow row = dt.NewRow();

        //        row.ItemArray = sValue.Split('^');

        //        dt.Rows.Add(row);
        //    }

        //    return dt;
        //}
    }
}