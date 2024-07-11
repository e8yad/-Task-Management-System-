using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static  class clsCategoriesData
    {
        // all type i need to be async 

        public static async Task<long?> AddNewCategoryAsync(long PersonID,string CatName)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_AddCat", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", CatName);
                    command.Parameters.AddWithValue("@prsonID", PersonID);
                    SqlParameter CatId = new SqlParameter("@CatID", System.Data.SqlDbType.BigInt)
                    {
                        Direction = System.Data.ParameterDirection.Output
                    };
                    command.Parameters.Add(CatId);
                    
                    try
                    {
                        await connection.OpenAsync();

                        int affectRaw=await command.ExecuteNonQueryAsync();
                        if (affectRaw>0)
                        {
                            return (long)CatId.Value;
                        }
                        else
                            // this means thic category is olready exists 
                            return null;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Failed to add new category",e);
                    }
                }
            }

        
        
        
        
        }

        public static async Task<DataTable> GetCategoriesRelatedToPersonAsync(long PersonID)
        {
            using(SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_CategoriesByPersonID", connection))
                {   
                    command.CommandType=System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID",PersonID);
                    DataTable t=new DataTable();
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader=await command.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                            t.Load(reader);

                        }

                        return t;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not load categories",e);
                    }

                }






            }




        }
        public static async Task<List<string>> GetCategoriesListRelatedToPersonAsync(long PersonID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_CategoriesByPersonID", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    List<string> t = new List<string>();
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            t.Add(reader["name"].ToString());

                        }

                        return t;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not load categories", e);
                    }

                }






            }




        }

        public static async Task<List<string>> GetTaskCategories(long TaskID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetTaskCategories", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TaskID", TaskID);
                    List<string> t = new List<string>();
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            t.Add((string)reader["Name"]);

                        }

                        return t;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not load categories", e);
                    }

                }



            }




        }



    }
}
