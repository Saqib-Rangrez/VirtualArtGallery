using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VirtualArtGallery.util
{
    static class DBConnUtil
    {
        static SqlConnection conn;

        public static SqlConnection GetConnection ()
        {
            try
            {                
                conn = new SqlConnection();
                conn.ConnectionString = DBPropUtil.GetConnectionString();                
                return conn;
            }catch (Exception e)
            {
                Console.WriteLine($"An error occurred at DbUtilConn: {e.Message}");
                throw new Exception("Connection Failed!");
            }
        }
    }
}
