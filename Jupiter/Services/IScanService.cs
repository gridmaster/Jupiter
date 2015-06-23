using System.Collections.Generic;
using System.ServiceModel;
using Jupiter.Models;
using Jupiter.Models.Requests;

namespace Jupiter.Services
{
    [ServiceContract]
    public interface IScanService
    {
        [OperationContract]
        string GetTradeResults(string symbol, int multipliar, decimal maxLoss, decimal profitSellStop, decimal splitTest, int numberOfMonths, int maxAge);

        [OperationContract]
        string FindPosition(string symbol);
    }
}
