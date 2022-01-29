using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Accounting.dbSource
{
    class dbHelper
    {
        public static string Getconnectionstring()
        {
            string val = ConfigurationManager.ConnectionStrings["Default Connection"].ConnectionString;
            return val;
        }

    }
}
