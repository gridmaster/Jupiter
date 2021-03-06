﻿using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Jupiter.Logs;

namespace Jupiter.Models.Context
{
    public class MarketsContext : DbContext
    {
        public DbSet<DailyTrade> Tickets { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ETFBaseData> ETFBaseData { get; set; }
        public DbSet<LogEvent> Logs { get; set; }

        //the base contains the name of the connection string provided in the web.config
        public MarketsContext()
            : base("MarketsContext")
        {
            //disable initializer
            Database.SetInitializer<MarketsContext>(null);
        }

        #region GetConnectionstring
        /// <summary>
        /// method to retrieve connection stringed in the web.config file
        /// </summary>
        /// <param name="str">Name of the connection</param>
        /// <remarks>Need a reference to the System.Configuration Namespace</remarks>
        /// <returns></returns>
        public static string GetConnectionString(string str)
        {
            //variable to hold our return value
            string conn = string.Empty;
            //check if a value was provided
            if (!string.IsNullOrEmpty(str))
            {
                //name provided so search for that connection
                conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            else
            //name not provided, get the 'default' connection
            {
                conn = ConfigurationManager.ConnectionStrings["MarketsContext"].ConnectionString;
            }
            //return the value
            return conn;
        }
        #endregion 
    }
}
