using System;

namespace Logger
{
    public class BaseLogger
    {
        public LogLevelEnum LogLevel { get; set; }

        private string TimeStamp
        {
            get
            {
                string timeStamp = string.Format("{0:HH:mm:ss.fff}", DateTime.Now);
                return timeStamp;
            }
        }
    }
}
