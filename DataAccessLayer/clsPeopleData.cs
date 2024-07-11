using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace DataAccessLayer
{
    public class clsPeopleData
    {
        // fully async 
       
        public static async Task<long?> AddNewPersonAsync(string Name,string Email,string Password,DateTime BirthDate)
        {

            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                SqlCommand Command = new SqlCommand("sp_AddNewPerson", Connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@Name", Name);
                Command.Parameters.AddWithValue("@Email", Email);
                Command.Parameters.AddWithValue("@BirthDate", BirthDate);
                Command.Parameters.AddWithValue("@Password", Password);
                SqlParameter outPutParamter = new SqlParameter("@PersonID", System.Data.SqlDbType.BigInt)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                Command.Parameters.Add(outPutParamter);

                try
                {
                    // wait until i open my connection 
                    await Connection.OpenAsync();
                 
                    // will not continue until it finsih its process
                    await Command.ExecuteNonQueryAsync();
                    if (outPutParamter .Value!=DBNull.Value && (long)outPutParamter.Value > 0)
                        return (long)outPutParamter.Value;
                    else return null;

                }
                catch (Exception e)
                {

                    throw new Exception("Cann not Add new person", e);
                }




            }





        }


        public static async Task<bool>  IsEmailExistAsync(string Email)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                SqlCommand command = new SqlCommand("sp_IsEmailExists", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email",Email);
                // will not continue before oppenc connection
                try
                {
                    await connection.OpenAsync();
                    object ReturnVlaue = await command.ExecuteScalarAsync();
                    if (ReturnVlaue != DBNull.Value && int.TryParse(ReturnVlaue.ToString(), out int Value))
                    {
                        if (Value == 1)
                            return true;
                    }
                }
                catch (Exception e)
                {

                    throw new Exception("Cann not Check Email", e);
                }

                return false;

            }

          


        }

        public static async Task<bool> UpdatePersonInfoAsync(long PerosnID,string Name,string Email,DateTime BirthDate)
        {
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                SqlCommand Command = new SqlCommand("sp_UpdatePerson", connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@PersonID", PerosnID);
                Command.Parameters.AddWithValue("@Name", Name);
                Command.Parameters.AddWithValue("@Email", Email);
                Command.Parameters.AddWithValue("@BirthDate", BirthDate);
                try
                {
                    await connection.OpenAsync();
                   int affectedRaws=await Command.ExecuteNonQueryAsync();
                    if (affectedRaws > 0)
                        return true;

                }
                catch (Exception e)
                {

                    throw new Exception("Cann not update person password", e);
                }
                // will continue after catch don't worry 
                return false;


            }




        }

        public static async Task<bool> UpdatePersonPasswordAsync(long PerosnID, string Password)
        {
            
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                SqlCommand Command = new SqlCommand("sp_UpdatePerson", connection);
                Command.CommandType = System.Data.CommandType.StoredProcedure;
                Command.Parameters.AddWithValue("@PersonID", PerosnID);
                Command.Parameters.AddWithValue("@Password", Password);
                try
                {
                    await connection.OpenAsync();
                    int affectedRaws = await Command.ExecuteNonQueryAsync();
                    if (affectedRaws > 0)
                        return true;

                }
                catch (Exception e)
                {

                    throw new Exception("Cann not update person password",e);
                }
                // will continue after catch don't worry 
                return false;


            }




        }


       public static bool Find(string Email,string Password,ref long PersonID,ref string Name,ref DateTime BirthDate)
        {
            bool ISFound = false;
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                using(SqlCommand Command = new SqlCommand("sp_FindPerson", connection))
                {
                    Command.CommandType= System.Data.CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@Email", Email);
                    Command.Parameters.AddWithValue("@Password", Password);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = Command.ExecuteReader();
                        if (reader.Read())
                        {
                            ISFound=true;
                            PersonID = (long)reader["PersonID"];
                            Name = (string)reader["Name"];
                            BirthDate = (DateTime)reader["BirthDate"];
                            reader.Close();
                        }
                        return ISFound;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Error in searching for person",ex);
                    }
                    
                }


            }




        }

        public static bool Find(ref string Email ,long PersonID, ref string Name, ref DateTime BirthDate)
        {
            bool ISFound = false;
            using (SqlConnection connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                using (SqlCommand Command = new SqlCommand("sp_FindByPersonID", connection))
                {
                    Command.CommandType = System.Data.CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@PersonID", PersonID);
          
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = Command.ExecuteReader();
                        if (reader.Read())
                        {
                            ISFound = true;
                            Email = (string)reader["email"];
                            Name = (string)reader["Name"];
                            BirthDate = (DateTime)reader["BirthDate"];
                            reader.Close();
                        }
                        return ISFound;
                    }
                    catch (Exception ex)
                    {

                        throw new Exception("Error in searching for person", ex);
                    }

                }


            }




        }

        

    }
}
