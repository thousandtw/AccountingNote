using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AccountingNote.dbSource
{
    public class dbHelper
    {
        public static string Getconnectionstring()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            return val;
        }
        public static DataTable ReadDataTable(string connectionstring, string dbCommandstring, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
            }
        }
        public static DataRow ReadDataRow(string connectionstring, string dbCommandstring, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    if (dt.Rows.Count == 0)
                        return null;
                    DataRow dr = dt.Rows[0];
                    return dr;
                }
            }
        }
        public static int ModifyData(string connectionstring, string dbCommandstring, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    int effectRowsCnt = command.ExecuteNonQuery();
                    return effectRowsCnt;
                }
            }
        }
        public static void CreateData(string connectionstring, string dbCommandstring, List<SqlParameter> list)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    command.Parameters.AddRange(list.ToArray());

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static DataTable GetDataTable(string connectionstring, string dbCommandstring)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand(dbCommandstring, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    reader.Close();

                    return dt;
                }
            }
        }
    }
}
