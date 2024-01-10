using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.util
{
    static class DBPropUtil
    {
        static string connectionString;

        public static string GetConnectionString()
        {
            connectionString = ConfigurationManager.ConnectionStrings["VirtualArtGallery"].ConnectionString;
            return connectionString;
        }
    }
}
