using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jupiter.Core;

namespace Jupiter.Models
{
    public class ETFBaseData : EtfBase
    {
        public override T LoadRow<T>(string[] rows)
        {
            if (rows.Length < 3)
                throw new ArgumentException("requires 3 rows to be passed.");

            Date = DateTime.Now;
            EtfName = rows[1];
            Ticker = rows[2];
            ExchangeId = 0;
            Exchange = rows[3];
            return this as T;
        }

        public override string GetURI()
        {
            return EtfUris.uriTradingVolume;
        }
    }
}