using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jupiter.Contracts
{
    public interface IStringWorks
    {
        string ReplaceFirstOccurrance(string original, string oldValue, string newValue);

        string fixComma(string value);

        string replaceComma(string value);

        string GetSetValues(string today, string todaysData);
    }
}