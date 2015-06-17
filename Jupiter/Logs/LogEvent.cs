using System;
using System.ComponentModel.DataAnnotations;

namespace Jupiter.Logs
{
    public class LogEvent
    {
        [Key]
        public int LogEventId { get; set; }

        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }

        public LogEvent(string location, string desc)
        {
            Location = location;
            Description = desc;
            Timestamp = DateTime.Now;
        }
    }
}