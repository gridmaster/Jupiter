using System;
using System.IO;
using System.Net;
using System.Text;

namespace Jupiter.Core
{
    public static class WebWorks
    {
        public static string GetResponse(string sUri)
        {
            var httpWRequest = (HttpWebRequest)WebRequest.Create(sUri);
            StreamReader reader = null;

            try
            {
                httpWRequest.KeepAlive = false;
                httpWRequest.ProtocolVersion = HttpVersion.Version10;

                var httpWebResponse = (HttpWebResponse)httpWRequest.GetResponse();
                var responseStream = httpWebResponse.GetResponseStream();

                if (responseStream != null)
                    reader = new StreamReader(responseStream, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                return "JsonWorks.GetResponse ERROR: " + ex.Message;
                //throw ex;
            }

            return reader == null ? "" : reader.ReadToEnd();
        }
    }
}