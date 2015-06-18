using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Jupiter.BulkLoad;
using Jupiter.Core;
using Jupiter.Models;

namespace Jupiter.Core
{
    public class ServiceWorks
    {
        #region tunable resources
        public static int multipliar = 100;
        public static decimal maxLoss = -0.04m;
        public static decimal profitSellStop = 0.99m;
        public static decimal splitTest = 200m;
        public static int numberOfMonths = -3;
        public static int maxAge = 30;

        #region buySellMatrix
        public static bool buyOnOpen;
        public static bool buyOnTrigger;
        public static bool buyOnClose;
        public static bool sellOnOpen;
        public static bool sellOnTrigger;
        public static bool sellOnClose;
        #endregion buySellMatrix

        #endregion tunable resources

        public static void RunQuickExit(string symbol)
        {
            Console.WriteLine("RunQuickExit: {0}", DateTime.Now);
            int skip = 0;
            var date = DateTime.Now;
            decimal up = 0m;
            decimal down = 0m;
            int age = 1;

            DailyTrades dts = new DailyTrades();

            //example of complete uri
            //http://real-chart.finance.yahoo.com/table.csv?s=VXX&d=5&e=27&f=2015&g=d&a=0&b=30&c=2009&ignore=.csv

            string uriString = WebWorks.GetUri(symbol, date, numberOfMonths);

            string today = DateTime.Now.Date.ToString().Substring(0, date.ToString().IndexOf(' '));
            string dayFile = string.Format(@"C:\Users\Jim\Documents\Visual Studio 2012\Projects\Jupiter\Files\QuickExit {0} - {1}.csv", symbol.ToUpper(), today.Replace('/', '-'));

            string sPage = WebWorks.Get(uriString);
            //string test = WebPage.TheDownload(uriString);
            string[] dailyArray = sPage.Split('\n');
            List<Daily> dailies = GetDailies(dailyArray);
            dailies.Reverse();

            using (StreamWriter sw = new StreamWriter(dayFile))
            {
                for (int iNdx = 1; iNdx < 5; iNdx++)
                {
                    Dictionary<DicKey, decimal> gainsLoses = new Dictionary<DicKey, decimal>();
                    List<Daily> positions = new List<Daily>();
                    Daily currentPosition = null;
                    Daily lastPosition = new Daily();

                    Daily dailyLow = new Daily()
                    {
                        Date = DateTime.Now,
                        Open = 0m,
                        Close = 0m,
                        High = 0m,
                        Low = 9999999.0m,
                        Volume = 0,
                        AdjClose = 0m
                    };
                    Daily longtermLow = new Daily();
                    longtermLow = dailyLow;
                    maxLoss = -.02m * iNdx; // -.02, -.04, -.06, -.08

                    for (int i = 0; i < dailies.Count; i++)
                    {
                        if (i < skip) continue;

                        if (longtermLow.Low > dailies[i].Low)
                        {
                            longtermLow = dailies[i];
                        }

                        if (age > maxAge)
                        {
                            // if we go maxAge with no new low, reset to the lowest number in the last 30
                            dailyLow = longtermLow;
                            //sw.WriteLine("{0} > {1}:, {2}, {3}, {4}, {5}, {6}", age, maxAge,
                            //        dailyLow.Date, dailyLow.Open, dailyLow.High, dailyLow.Low, dailyLow.Close);
                        }

                        // todays close / yesterdays close * 100 > 300 == split so restart calculations
                        if ((lastPosition.Close != 0 && ((lastPosition.Close / dailies[i].Close) * 100) > splitTest))
                        {
                            positions.Add(currentPosition);
                            DicKey dk = new DicKey()
                            {
                                Date = currentPosition.Date,
                                MaxLoss = maxLoss
                            };
                            gainsLoses.Add(dk, 0m);
                            currentPosition = null;
                            dts.Add(WriteOutput(sw, symbol, dailies[i], currentPosition, 0.0m, dailyLow.Low, maxLoss));
                            dailyLow = dailies[i];
                        }

                        // if we hit a new low
                        if (dailyLow.Low > dailies[i].Low)
                        {
                            dailyLow = dailies[i];

                            // if we don't have a position, start one
                            if (currentPosition == null)
                            {
                                currentPosition = new Daily
                                {
                                    Date = dailies[i].Date,
                                    Open = dailies[i].Open,
                                    High = dailies[i].High,
                                    Low = dailies[i].Low,
                                    Close = dailies[i].Close,
                                    Volume = dailies[i].Volume,
                                    AdjClose = dailies[i].AdjClose
                                };
                            }

                            lastPosition = dailies[i];

                            age = 1; // reset age
                            longtermLow = dailyLow; // reset long term low
                            continue;
                        }

                        if (currentPosition != null)
                        {
                            // if we gain more than a $1
                            if (((decimal)dailies[i].High - (decimal)currentPosition.Low) > profitSellStop)
                            {

                                // if all is well sell at close
                                decimal gain = ((decimal)dailies[i].Close - (decimal)currentPosition.Low);
                                if (gain > profitSellStop)
                                {
                                    DicKey dk = new DicKey()
                                    {
                                        Date = dailies[i].Date,
                                        MaxLoss = maxLoss
                                    };
                                    gainsLoses.Add(dk, gain);
                                    up += gain;
                                    dts.Add(WriteOutput(sw, symbol, dailies[i], currentPosition, gain, currentPosition.Low, maxLoss));
                                    //dt.BuyDate = dailies[i].Date;
                                }
                                else // this should not execute... I don't think, we now sell at close.
                                {   // profitSellStop + .01m because we actually test for > profitSellStop
                                    DicKey dk = new DicKey()
                                    {
                                        Date = dailies[i].Date,
                                        MaxLoss = maxLoss
                                    };
                                    gainsLoses.Add(dk, profitSellStop + .01m);
                                    up += profitSellStop + .01m;
                                    dts.Add(WriteOutput(sw, symbol, dailies[i], currentPosition, profitSellStop + .01m, dailyLow.Low, maxLoss));
                                }
                                positions.Add(currentPosition);
                                currentPosition = null;
                            }
                            else
                            {
                                // if we hit the maxLoss sell stop..
                                decimal LossSellStop = (decimal.Divide((decimal)dailies[i].Low, (decimal)currentPosition.Low) - 1);
                                if (LossSellStop < maxLoss)
                                {
                                    decimal loss = dailies[i].Open - currentPosition.Low;
                                    positions.Add(currentPosition);

                                    // if open is > loss than maxLoss sell at open
                                    if ((decimal.Divide((decimal)dailies[i].Open, (decimal)currentPosition.Low) - 1) < maxLoss)
                                    {
                                        DicKey dk = new DicKey()
                                        {
                                            Date = dailies[i].Date,
                                            MaxLoss = maxLoss
                                        };
                                        gainsLoses.Add(dk, loss);
                                        down += loss;
                                        dts.Add(WriteOutput(sw, symbol, dailies[i], currentPosition, loss, dailyLow.Low, maxLoss));
                                    }
                                    else
                                    {// else sell at maxLoss
                                        loss = (currentPosition.Low * (1m - maxLoss)) - currentPosition.Low;
                                        DicKey dk = new DicKey()
                                        {
                                            Date = dailies[i].Date,
                                            MaxLoss = maxLoss
                                        };
                                        gainsLoses.Add(dk, loss);
                                        down += loss;
                                        dts.Add(WriteOutput(sw, symbol, dailies[i], currentPosition, loss, dailyLow.Low, maxLoss));
                                    }
                                    currentPosition = null;
                                }
                            }
                        }

                        //last thing to do...
                        lastPosition.Date = dailies[i].Date;
                        lastPosition.Open = dailies[i].Open;
                        lastPosition.High = dailies[i].High;
                        lastPosition.Low = dailies[i].Low;
                        lastPosition.Close = dailies[i].Close;
                        lastPosition.Volume = dailies[i].Volume;
                        lastPosition.AdjClose = dailies[i].AdjClose;
                       // dts.Add(WriteOutput(sw, symbol, lastPosition, lastPosition, 0, dailyLow.Low, maxLoss));
                    }  // end for
                }  // end for
            }
            //dts.Add(WriteOutput(sw, symbol, lastPosition, lastPosition, 0, dailyLow.Low, maxLoss));
            using (BulkLoadDailyTrades bls = new BulkLoadDailyTrades())
            {
                var dt = bls.ConfigureDataTable();
                dt = bls.LoadDataTableWithDailyTrades(dts, dt);
                bls.BulkCopy<DailyTrades>(dt);
            }
        }

        #region private methods
        private static string GetOutPutFile(string symbol, string name)
        {
            string today = DateTime.Now.Date.ToString().Substring(0, DateTime.Now.ToString().IndexOf(' '));
            return string.Format(@"C:\Users\Jim\Documents\Visual Studio 2012\Projects\LoadSectorIndustrySymbol\Files\{0} {1} - {2}.csv", name, symbol.ToUpper(), today.Replace('/', '-'));
        }

        private static List<Daily> GetDailyValues(string symbol)
        {
            var date = DateTime.Now;

            string uriString = WebWorks.GetUri(symbol, date, numberOfMonths);

            //string[] dailyArray = System.IO.File.ReadAllLines(@"../../../Files/vxxtable.csv");
            string sPage = WebWorks.Get(uriString);
            string[] dailyArray = sPage.Split('\n');
            List<Daily> dailies = GetDailies(dailyArray);
            dailies.Reverse();
            return dailies;
        }

        public static DailyTrade WriteOutput(StreamWriter sw, string symbol, Daily today, Daily current, decimal value, decimal low, decimal maxPain)
        {
            DailyTrade dtrade = new DailyTrade()
            {
                Ticker = symbol,
                MaxPain = maxPain,
                BuyDate = current.Date,
                BuyOpen = current.Open,
                BuyHigh = current.High,
                BuyLow = current.Low,
                BuyClose = current.Close,
                BuyVolume = current.Volume,
                SellDate = today.Date,
                SellOpen = today.Open,
                SellHigh = today.High,
                SellLow = today.Low,
                SellClose = today.Close,
                SellVolume = today.Volume,
                TradeValue = value,
                CurrentLow = low
            };

            sw.WriteLine("MaxPain:, {12} - Bought:, {0}, {1}, {2}, {3}, {4}, Sold:, {5}, {6}, {7}, {8}, {9}, Low:, {10}, Value:, {11}",
                current.Date, current.Open, current.High, current.Low, current.Close,
                today.Date, today.Open, today.High, today.Low, today.Close, low, value, maxPain);


            return dtrade;
        }

        public static void WriteOutput(StreamWriter sw, Daily today, Daily current, decimal value, decimal low)
        {
            if (current == null)
            {
                sw.WriteLine("Stock split:, {0}, {1}, {2}, {3}, {4}, Low: {5}",
                        today.Date, today.Open, today.High, today.Low, today.Close, low);
            }
            else
            {
                decimal newMultipliar = multipliar;
                if (current.Low < 30m) newMultipliar = multipliar * 2;

                sw.WriteLine("Bought:, {0}, {1}, {2}, {3}, {4}, Sold:, {5}, {6}, {7}, {8}, {9}, Low:, {10}, Value:, {11}",
                    current.Date, current.Open, current.High, current.Low, current.Close,
                    today.Date, today.Open, today.High, today.Low, today.Close, low, value * newMultipliar);
            }
        }

        private static List<Daily> GetDailies(string[] dailyArray)
        {
            List<Daily> dailies = new List<Daily>();

            foreach (string value in dailyArray)
            {
                if (string.IsNullOrEmpty(value)) return dailies;

                string[] line = value.Split(',');

                if (line[0] == "Date") continue;

                Daily daily = new Daily();
                daily.Date = Convert.ToDateTime(line[0]);
                daily.Open = Convert.ToDecimal(line[1]);
                daily.High = Convert.ToDecimal(line[2]);
                daily.Low = Convert.ToDecimal(line[3]);
                daily.Close = Convert.ToDecimal(line[4]);
                daily.Volume = Convert.ToInt32(line[5]);
                daily.AdjClose = Convert.ToDecimal(line[6]);
                dailies.Add(daily);
            }
            return dailies;
        }
        #endregion private methods
    }
}