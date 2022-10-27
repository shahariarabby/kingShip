using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QMSWebAPI.Utilities
{
    public class DBConnection
    {
        public static string _connectionString;
        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(DBConnection._connectionString))
            {
                DBConnection._connectionString = ConfigurationManager.ConnectionStrings["Energy"].ConnectionString;
            }
            return DBConnection._connectionString;
        }
        public static string GetConnectionStringIncentive()
        {
            if (string.IsNullOrEmpty(DBConnection._connectionString))
            {
                DBConnection._connectionString = ConfigurationManager.ConnectionStrings["IncentiveDB"].ConnectionString;
            }
            return DBConnection._connectionString;
        }
        public static string GetConnectionStringKinship()
        {
            if (string.IsNullOrEmpty(DBConnection._connectionString))
            {
                DBConnection._connectionString = ConfigurationManager.ConnectionStrings["Kinship"].ConnectionString;
            }
            return DBConnection._connectionString;
        }
    }
}