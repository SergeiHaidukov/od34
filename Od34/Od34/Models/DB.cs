using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Od34.Models
{
    public class DB
    {
        public static SqlConnection conn;
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public DB()
        {
            conn = new SqlConnection(ConnectionString);
        }        

        //static IDbConnection connection;        

        //public static IDbConnection GetConnection()
        //{
        //    if (connection == null)            
        //        connection = new SqlConnection(ConnectionString);            
            

        //    if(connection.State != ConnectionState.Open)
        //        connection.Open();

        //    return connection;
        //}

        public static bool Connect()
        {
            if(conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch(Exception e)
                {

                }
            }

            if (conn.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
    }
}