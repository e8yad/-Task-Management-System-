using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DataAccessLayer
{
    public static class clsTasksData
    {
        // you did not handle null case 
        public static async Task<long?> AddTaskAsync(string TaskName,DateTime DueDate,string Notes ,long PersonID )
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using(SqlCommand  command = new SqlCommand("sp_AddTask", connection)) {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TaskName", TaskName);
                    command.Parameters.AddWithValue("@DueDate", DueDate);
                    command.Parameters.AddWithValue("@Notes", Notes);
                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    var TaskID=new SqlParameter("@TaskID",SqlDbType.BigInt)
                    {
                        Direction= ParameterDirection.Output,
                    };
                    command.Parameters.Add(TaskID);
                    try
                    {
                        await connection.OpenAsync();
                        object OutPut=await command.ExecuteScalarAsync();
                        if (OutPut != null)
                        {
                            return (long?)OutPut;
                        }
                        else
                            return null;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not add task to data base",e);
                    }
                
                
                }
            }
            


        }

        public static async Task<bool> UpdateStatusAsync(Byte StateID,long TaskID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_UpdateTaskState", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@state", StateID);
                    command.Parameters.AddWithValue("@TaskID", TaskID);
                    
                    try
                    {
                        await connection.OpenAsync();
                        if (await command.ExecuteNonQueryAsync()>0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not Update state of task", e);
                    }


                }
            }



        }


        public static async Task<bool> UpdateTaskNoteAsync(string Note, long TaskID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_UpdateTaskState", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Note", Note);
                    command.Parameters.AddWithValue("@TaskID", TaskID);

                    try
                    {
                        await connection.OpenAsync();
                        if (await command.ExecuteNonQueryAsync() > 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not Update note of task", e);
                    }


                }
            }



        }

        public static async Task<bool> AddCategoryToTask(long CategoryID, long TaskID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_UpdateTaskState", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CatID", CategoryID);
                    command.Parameters.AddWithValue("@TaskID", TaskID);

                    try
                    {
                        await connection.OpenAsync();
                        if (await command.ExecuteNonQueryAsync() > 0)
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not add Category to task", e);
                    }


                }
            }



        }

        public static async Task<DataTable> TasksByPersonIDAsync(long PeronID)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("sp_TasksByPersonID", connection))
                {

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PeronID);
                    DataTable dt = new DataTable();
                    try
                    {
                        await connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        if(reader.HasRows)
                        {
                             dt.Load(reader);
                        }
                        return dt;

                    }
                    catch (Exception e)
                    {

                        throw new Exception("Can not add Category to task", e);
                    }


                }
            }
        }




    }
}
