using System.Data;
using System.Data.SqlClient;
using Tool_Shop_Web_App.Common;
using Tool_Shop_Web_App.Models;

namespace Tool_Shop_Web_App.UserMode
{
    public class UserServices : IDisposable
    {
        public void Dispose() { }

        public DataTable FetchUserDetails(string userName)
        {
            DataTable userDetails = new DataTable();
            try
            {
                using (Connection objConnection = new Connection())
                {
                    // Use parameterized query to prevent SQL injection
                    string query = "SELECT * FROM UserMaster WHERE Username = @Username";
                    var parameters = new Dictionary<string, object>
                    {
                        { "@Username", userName }
                    };
                    userDetails = objConnection.FetchDataTableFromQuery(query, parameters);
                }
            }
            catch (Exception ex)
            {
                // Write Error Log
            }
            return userDetails;

        }

        public DataTable FetchRecord(string strTableName, string Id)
        {
            var parameters = new Dictionary<string, object>
                    {
                        { "@ID", Id}
                    };
            string query = $"Select * from {strTableName} where Id =@ID";
            using (Connection objCOnnection = new Connection())
            {
                return objCOnnection.FetchDataTableFromQuery(query, parameters);
            }
        }
        public DataTable FetchItems()
        {
            DataTable items = new DataTable();
            try
            {
                using (Connection objConnection = new Connection())
                {
                    items = objConnection.FetchDataTableFromQuery("Select * From Items", null);
                }

            }
            catch (Exception ex)
            {
            }
            return items;
        }
        public DataTable GetCategories()
        {
            DataTable categories = new DataTable();
            try
            {
                using (Connection objConnection = new Connection())
                {
                    string Query = "SELECT * FROM Category";
                    categories = objConnection.FetchDataTableFromQuery(Query, null);
                }

            }
            catch { }
            return categories;
        }

        public bool ItemExists(string Code)
        {
            DataTable Items = new DataTable();
            try
            {
                var parameters = new Dictionary<string, object>
                    {
                        { "@Code", Code},
                        {"@IsFreezed",0 }
                    };
                using (Connection objConnection = new Connection())
                {
                    Items = objConnection.FetchDataTableFromQuery($"SELECT * FROM ITEMS WHERE CODE=@Code and IsFreezed = @IsFreezed", parameters);

                }
            }
            catch (Exception ex)
            {

            }
            return Items.Rows.Count > 0;

        }

        public DataTable SaveItem(ItemDM item)
        {

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id",item.Id),
                new SqlParameter("@Code", item.Code),
                new SqlParameter("@Name", item.Name),
                new SqlParameter("@Category", item.Category),
                new SqlParameter("@Price", item.Price),
                new SqlParameter("@Description", item.Description),
                new SqlParameter("@IsFreezed", item.IsFreezed),
                new SqlParameter("@Image", item.Image)
            };

            using (Connection objConnection = new Connection())
            {
                return objConnection.ExecuteStoreProcedure("SaveItem", parameters);
            }
        }

        public bool DeleteItem(string id)
        {
            bool Status = false;
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter ("@Id",id)
                };
                using (Connection objconnection = new Connection())
                {
                    var res = objconnection.ExecuteStoreProcedure("DeleteItem", parameters);
                    if (res.Rows.Count > 0)
                    {
                        Status = bool.Parse(res.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
                //Write Error Log
            }
            return Status;
        }
    }
}