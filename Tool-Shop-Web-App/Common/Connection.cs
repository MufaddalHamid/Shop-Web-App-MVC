using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tool_Shop_Web_App.Common
{
    public class Connection:IDisposable
    {
        public void Dispose() { }
        private string connectionString = ConfigurationManager.AppSettings["LocalShopDB"];

        public DataTable FetchDataTableFromQuery(string query, Dictionary<string, object> parameters)
        {
            DataTable table = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            // Add parameters to the command
                            foreach (var param in parameters)
                            {
                                command.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Write Error Log
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return table;
        }


        public DataTable ExecuteStoreProcedure(string procedureName, SqlParameter[] parameters)
        {
            DataTable dttable = new DataTable();
            // Ensure to dispose of the connection and command objects properly
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add the parameters to the command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dttable);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw;
                        // Handle the exception (e.g., log it)

                    }
                }
            }
            return dttable;
        }

    }
}