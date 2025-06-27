using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCuaHangDoAnNhanh.UserControls;
using System.Text.RegularExpressions;

namespace QuanLyCuaHangDoAnNhanh.DAO
{
    class DataProvider
    {
        private static DataProvider instance;

        // Singleton property to get the single instance of DataProvider
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return instance; }
            private set { instance = value; }
        }

        private DataProvider() { }

        // Connection string to the database, retrieved from application settings.
        private string connString = Properties.Settings.Default.ConnectionString;

        // Executes a SQL query and returns the result as a DataTable.
        public DataTable ExecuteQuery(string query, object[] parameter = null) 
        {
            DataTable data = new DataTable();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    var matches = Regex.Matches(query, @"@\w+");
                    var paramNames = matches.Cast<Match>().Select(m => m.Value).Distinct().ToList();

                    for (int i = 0; i < paramNames.Count && i < parameter.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(paramNames[i], parameter[i]);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

                adapter.Fill(data);

                conn.Close();
            }
            return data;
        }

        // Executes a non-query SQL command (INSERT, UPDATE, DELETE) and returns the number of rows affected.
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    var matches = Regex.Matches(query, @"@\w+");
                    var paramNames = matches.Cast<Match>().Select(m => m.Value).Distinct().ToList();

                    for (int i = 0; i < paramNames.Count && i < parameter.Length; i++)
                    {
                        sqlCommand.Parameters.AddWithValue(paramNames[i], parameter[i]);
                    }
                }

                data = sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
            return data;
        }

        // Executes a SQL command that returns a single value (e.g., COUNT, MAX) and returns that value.
        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            sqlCommand.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = sqlCommand.ExecuteScalar();
                conn.Close();
            }
            return data;
        }
    }
}
