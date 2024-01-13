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

    public static class DBConnUtil
    {
        private static string connectionString;

        static DBConnUtil()
        {
            try
            {
                connectionString = DBPropUtil.GetConnectionString();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during static initialization: {e.Message}");
                throw new Exception("Connection Failed!");
            }
        }

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred at DbUtilConn: {e.Message}");
                throw new Exception("Connection Failed!");
            }
        }
    }

}
