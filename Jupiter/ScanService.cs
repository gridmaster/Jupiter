using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text.RegularExpressions;
using System.Xml;
using Newtonsoft.Json;
using Jupiter.BulkLoad;
using Jupiter.Core;
using Jupiter.Logs;
using Jupiter.Models;
using Jupiter.Models.Context;
using Jupiter.Models.Requests;

namespace Jupiter
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ScanService : IScanService
    {
        private static MarketsContext db = new MarketsContext();

        [WebGet(UriTemplate =
            "/ScanService?symbol={symbol}&multipliar={multipliar}&maxLoss={maxLoss}"
            + "&profitSellStop={profitSellStop}&splitTest={splitTest}"
            + "&numberOfMonths={numberOfMonths}&maxAge={maxAge}"
            , ResponseFormat = WebMessageFormat.Json)]
        public string GetTradeResults(string symbol, int multipliar, decimal maxLoss, decimal profitSellStop, decimal splitTest, int numberOfMonths, int maxAge)
        {
            //Log.WriteLog(new LogEvent(string.Format("QuickPickService - QuickPicks()"), "Start"));

            ServiceWorks.RunQuickExit("WAB");

            //Tickets tixs = new Tickets();
            //tixs.tickets = new List<Ticket>();

            return "what do you want?";
        }
        
        #region private methods
        private static int fixTix(int tix)
        {
            if (tix == 0) tix = 1;
            if (tix > 20) tix = 20;
            return tix;
        }

        private static String fixParam(String param)
        {
            if (param == null) param = "";
            return param;
        }

        private static void WriteLog(string message)
        {
            using (StreamWriter log = File.AppendText("logs/log.txt"))
            {
                log.Write(DateTime.Now.ToString(CultureInfo.InvariantCulture) + ": " + message + "\r\n");
                log.Flush();
                log.Close();
            }
        }
        #endregion private methods

    }
}