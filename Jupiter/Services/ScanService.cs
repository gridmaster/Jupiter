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
using DIContainer;
using Newtonsoft.Json;
using Ninject;
using Jupiter.BulkLoad;
using Jupiter.Core;
using Jupiter.Logs;
using Jupiter.Models;
using Jupiter.Models.Context;
using Jupiter.Models.Requests;
using Jupiter.DIModule;

namespace Jupiter.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ScanService : IScanService
    {
        private static MarketsContext db = new MarketsContext();
        #region initialize DI container

        private static void InitializeDiContainer()
        {
            NinjectSettings settings = new NinjectSettings
            {
                LoadExtensions = false
            };

            // change DesignTimeModule to run other implementation ProductionModule || DebugModule
            IOCContainer.Instance.Initialize(settings, new DebugModule());
        }

        #endregion Initialize DI Container
        [WebGet(UriTemplate =
            "/ScanService?symbol={symbol}&multipliar={multipliar}&maxLoss={maxLoss}"
            + "&profitSellStop={profitSellStop}&splitTest={splitTest}"
            + "&numberOfMonths={numberOfMonths}&maxAge={maxAge}"
            , ResponseFormat = WebMessageFormat.Json)]
        public string GetTradeResults(string symbol, int multipliar, decimal maxLoss, decimal profitSellStop, decimal splitTest, int numberOfMonths, int maxAge)
        {
            //Log.WriteLog(new LogEvent(string.Format("QuickPickService - QuickPicks()"), "Start"));
            
            if( string.IsNullOrEmpty(symbol)) {
                ServiceWorks.RunQuickExit();
            }
            else {
                ServiceWorks.RunQuickExit(symbol);
            }
            return "Were you looking for something?";
        }
        
        [WebGet(UriTemplate = "/ScanService/FindPosition?symbol={symbol}", ResponseFormat = WebMessageFormat.Json)]
        public string FindPosition(string symbol)
        {
            if (string.IsNullOrEmpty(symbol))
            {
                ServiceWorks.FindPositionToOpen();
            }
            else
            {
                ServiceWorks.FindPositionToOpen(symbol);
            }
            return "The check is in the mail";
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