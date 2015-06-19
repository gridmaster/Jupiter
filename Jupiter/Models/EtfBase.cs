using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jupiter.Models
{
    public abstract class EtfBase
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        // ETF NAME
        public string EtfName { get; set; }

        //TICKER
        public string Ticker { get; set; }

        public int ExchangeId { get; set; }

        public string Exchange { get; set; }

        public abstract T LoadRow<T>(string[] rows) where T : class;

        public abstract string GetURI();
    }
}