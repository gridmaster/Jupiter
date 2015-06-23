using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using Jupiter.Models;


namespace Jupiter.BulkLoad
{
    public class BulkLoadOpenPositions : BaseBulkLoad, IDisposable
    {
        private static readonly string[] ColumnNames = new string[]
            {
                "Ticker", "BuyDate", "MaxPain", "BuyOpen",
                "BuyHigh", "BuyLow", "BuyClose", "BuyVolume", 
                "SellDate", "SellOpen", "SellHigh",
                "SellLow", "SellClose", "SellVolume", "TradeValue", "CurrentLow"
            };

        public BulkLoadOpenPositions()
            : base(ColumnNames)
        {

        }

        public DataTable LoadDataTableWithOpenPositions(IEnumerable<OpenPosition> dStats, DataTable dt)
        {
            foreach (var value in dStats)
            {
                var sValue = value.Ticker + "^" + value.BuyDate + "^" + value.MaxPain + "^" + value.BuyOpen
                                              + "^" + value.BuyHigh + "^" + value.BuyLow + "^" + value.BuyClose + "^" + value.BuyVolume
                                              + "^" + value.SellDate + "^" + value.SellOpen + "^" + value.SellHigh
                                              + "^" + value.SellLow + "^" + value.SellClose + "^" + value.SellVolume + "^" + value.TradeValue
                                               + "^" + value.CurrentLow;
                DataRow row = dt.NewRow();

                row.ItemArray = sValue.Split('^');

                dt.Rows.Add(row);
            }

            return dt;
        }

        #region Implement IDisposable
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        //More Info

        // Dispose() calls Dispose(true)
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~BulkLoadOpenPositions()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }
        }
        #endregion Implement IDisposable
    }
}