using System;

namespace Jupiter.Core
{
    public class StringWorks
    {
        private static string PlaceHolder
        {
            get { return "!^$^!"; }
        }

        private static string Ampersand
        {
            get { return "&amp;"; }
        }

        public static string ReplaceFirstOccurrance(string original, string oldValue, string newValue)
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

        public static string fixComma(string value)
        {
            var firstComma = value.IndexOf(",");
            var secondQuote = value.Substring(1).IndexOf("\"");
            if (firstComma < secondQuote)
            {
                return value.Substring(0, firstComma) + PlaceHolder + value.Substring(firstComma + 1);
            }

            return value;
        }

        public static string replaceComma(string value)
        {
            return value.Replace(PlaceHolder, ",");
        }
    }
}