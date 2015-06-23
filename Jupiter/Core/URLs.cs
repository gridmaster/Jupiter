
namespace Jupiter.Core
{
    public class URLs
    {
        public static string GetSectorIndustryUrl(string index)
        {
            return "http://finance.yahoo.com/q/in?s=" + index + "+Industry";
        }
       
        public static string GetSectorCsv
        {
            get { return "http://biz.yahoo.com/p/csv/s_conameu.csv"; }
        }
        
        public static string GetSectorUri
        {
            get { return "http://biz.yahoo.com/p/sectors.html"; }
        }

        public static string GetIndustryCsv
        {
            get { return "http://biz.yahoo.com/p/csv/sum_conameu.csv"; }
        }

        public static string GetIndustryBySectorCsv
        {
            get { return "http://biz.yahoo.com/p/csv/!^$^!conameu.csv"; }
        }

        public static string GetIndustryUri
        {
            get { return "http://biz.yahoo.com/p/industries.html"; }
        }
       
        public static string GetSymbolsUrl
        {
            get
            {
                return "http://biz.yahoo.com/i/";
            }
        }

        public static string GetKeyStatsURL(string index)
        {
            return "http://finance.yahoo.com/q/ks?s=" + index + "+Key+Statistics";
        }

        public static string GetOptionsURL(string index)
        {
            return "http://finance.yahoo.com/q/op?s=" + index + "+Options";
        }

        public static string BaseData()
        {
            return "http://biz.yahoo.com/p/";
        }

        public static string CurrentData()
        {
            return "http://finance.yahoo.com/q?s={0}";
        }
    }
}
