using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Jupiter.Models.Response
{
    public sealed class TradeHistoryResponse : BaseResponseData
    {
        [JsonProperty(PropertyName = "Tickets")]
        public IList<DailyTrade> TradeHistory;
    }
}