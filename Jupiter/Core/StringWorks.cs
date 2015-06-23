using System;
using System.Text.RegularExpressions;
using Jupiter.Contracts;

namespace Jupiter.Core
{
    public class StringWorks : IStringWorks
    {
        #region implimentation 
        public string ReplaceFirstOccurrance(string original, string oldValue, string newValue)
        {
            if (String.IsNullOrEmpty(original))
                return String.Empty;
            if (String.IsNullOrEmpty(oldValue))
                return original;
            if (String.IsNullOrEmpty(newValue))
                newValue = String.Empty;

            int loc = original.IndexOf(oldValue);
            return original.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }

        public string fixComma(string value)
        {
            var firstComma = value.IndexOf(",");
            var secondQuote = value.Substring(1).IndexOf("\"");
            if (firstComma < secondQuote)
            {
                return value.Substring(0, firstComma) + PlaceHolder + value.Substring(firstComma + 1);
            }

            return value;
        }

        public string replaceComma(string value)
        {
            return value.Replace(PlaceHolder, ",");
        }

        public string GetSetValues(string today, string todaysData)
        {
            //<div[^<>]*class="entry"[^<>]*>(?<content>.*?)</div>
            string setValue = today.Replace('/', '-');
            string[] parts = setValue.Split('-');

            setValue = parts[2] + "-" + (parts[0].Length == 1 ? "0" + parts[0] : parts[0]) + "-" + (parts[1].Length == 1 ? "0" + parts[1] : parts[1]);

            todaysData = todaysData.Substring(todaysData.IndexOf("yfi_quote_summary_data"));
            todaysData = todaysData.Substring(todaysData.IndexOf("<table"));
            todaysData = todaysData.Substring(0, todaysData.IndexOf("</div"));
            string[] rows = Regex.Split(todaysData, @"<tr>");
            string row = rows[2];
            row = row.Substring(row.IndexOf("<td"));
            row = row.Substring(row.IndexOf(">") + 1);
            string open = row.Substring(0, row.IndexOf("<"));
            if (open.Length == 0 || (open.IndexOf("N/A") > -1)) return string.Empty;
            setValue = setValue + "," + open;
            row = rows[8];
            row = row.Substring(row.IndexOf("<span") + "<span".Length);
            row = row.Substring(row.IndexOf("<span") + "<span".Length);
            row = row.Substring(row.IndexOf(">") + 1);
            string low = row.Substring(0, row.IndexOf("<"));
            if (low.Length == 0 || (low.IndexOf("N/A") > -1)) return string.Empty;
            row = row.Substring(row.IndexOf("<span") + "<span".Length);
            row = row.Substring(row.IndexOf("<span") + "<span".Length);
            row = row.Substring(row.IndexOf(">") + 1);
            string hi = row.Substring(0, row.IndexOf("<"));
            if (hi.Length == 0 || (hi.IndexOf("N/A") > -1)) return string.Empty;
            setValue = setValue + "," + hi + "," + low + ",0,0,0";
            return setValue;
        }
        #endregion implimentation

        #region private methods
        private static string PlaceHolder
        {
            get { return "!^$^!"; }
        }

        private static string Ampersand
        {
            get { return "&amp;"; }
        }
        #endregion private methods
    }
}