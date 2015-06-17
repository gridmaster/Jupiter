using System.Collections.Generic;
using Jupiter.Models.Context;

namespace Jupiter.Logs
{
    public class Log : List<LogEvent>
    {
        public int LogId { get; set; }

        public static void WriteLog(LogEvent le)
        {
            var db = new MarketsContext();

            db.Logs.Add(le);
            db.SaveChanges();
        }
    }
}