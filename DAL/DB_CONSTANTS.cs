using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DB_CONSTANTS
    {
        private static string connString_ICS = ConfigurationManager.AppSettings["connection_ICS"].ToString();
        //private static string connString_master = ConfigurationManager.ConnectionStrings["connection_master"].ConnectionString;


        public static string ConnectionString_ICS
        {
            get { return connString_ICS; }
        }

        //public static string ConnectionString_master
        //{
        //    get { return connString_master; }
        //}
    }
}
