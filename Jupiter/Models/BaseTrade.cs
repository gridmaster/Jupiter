using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jupiter.Models
{
    public class BaseTrade
    {
        public int Id { get; set; }

        public string Ticker { get; set; }
        public DateTime BuyDate { get; set; }
        public decimal MaxPain { get; set; }
        public decimal BuyOpen { get; set; }
        public decimal BuyHigh { get; set; }
        public decimal BuyLow { get; set; }
        public decimal BuyClose { get; set; }
        public decimal BuyVolume { get; set; }
        public DateTime SellDate { get; set; }
        public decimal SellOpen { get; set; }
        public decimal SellHigh { get; set; }
        public decimal SellLow { get; set; }
        public decimal SellClose { get; set; }
        public decimal SellVolume { get; set; }
        public decimal CurrentLow { get; set; }
        public decimal TradeValue { get; set; }
    }
}