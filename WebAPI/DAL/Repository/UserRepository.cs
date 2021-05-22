using DAL.IRepository;
using DAL.Models;
using DAL.Settings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepository:IUserRepository
    {
        public async Task AddUserAsync(User obj)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSettings.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    string sql = string.Format($"INSERT INTO [dbo].[User]  ([UserName] ,[Password] ,[FirstName] ,[LastName] ,[PhoneNo] ,[Email] ,[UserTypeId],[CompanyId] ,[IsAdmin] ,[IsActive]  ,[ActivationCode])   VALUES ('{obj.UserName}','{obj.Password}', '{obj.FirstName}', '{obj.LastName}','{obj.PhoneNo}','{obj.Email}', '{2}', '{obj.CompanyId}','{0}','{1}','{obj.ActivationCode}')");

                    sql = sql + " Select Scope_Identity()";
                    command.CommandText = sql;
                    var userId = await command.ExecuteScalarAsync();
                    
                    // Attempt to commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task DeleteUserAsync(long id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSettings.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    var sql = string.Format("DELETE FROM [dbo].[User] WHERE  id = '{0}'", id);
                    command.CommandText = sql;
                    await command.ExecuteNonQueryAsync();

                    // Attempt to commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);


            var commandText = string.Format("SELECT [Id] ,[UserName] ,[Password] ,[FirstName] ,[LastName] ," +
                "[PhoneNo] ,[Email] ,[UserTypeId],[CompanyId],[IsAdmin] ,[IsActive] ,[ActivationCode] FROM [dbo].[User]  WITH(NOLOCK)");

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var user = new User();
                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.UserName = Convert.ToString(dataReader["UserName"]).ToLower();
                    user.FirstName = Convert.ToString(dataReader["FirstName"]).ToLower();
                    user.Password = Convert.ToString(dataReader["Password"]).ToLower();
                    user.LastName = Convert.ToString(dataReader["LastName"]);
                    user.PhoneNo = Convert.ToString(dataReader["PhoneNo"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    user.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    user.UserTypeId = Convert.ToInt32(dataReader["UserTypeId"]);
                    user.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);
                    user.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    user.ActivationCode = Convert.ToString(dataReader["ActivationCode"]);
                    users.Add(user);
                }
                dataReader.Close();
                conn.Close();
            }
           
            return users;
        }

        public async Task<User> GetUserPropertyAsync(string userName)
        {
            var user = new User();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);


            var commandText = string.Format("SELECT [Id] ,[UserName] ,[Password] ,[FirstName] ,[LastName] ," +
                "[PhoneNo] ,[Email] ,[UserTypeId],[CompanyId],[IsAdmin] ,[IsActive] ,[ActivationCode] FROM [User]  WITH(NOLOCK) WHERE UserName ='{0}' ", userName);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.UserName = Convert.ToString(dataReader["UserName"]).ToLower();
                    user.FirstName = Convert.ToString(dataReader["FirstName"]).ToLower();
                    //user.Password = Convert.ToString(dataReader["Password"]).ToLower();
                    user.LastName = Convert.ToString(dataReader["LastName"]);
                    user.PhoneNo = Convert.ToString(dataReader["PhoneNo"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    user.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    user.UserTypeId = Convert.ToInt32(dataReader["UserTypeId"]);
                    user.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);
                    user.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    user.ActivationCode = Convert.ToString(dataReader["ActivationCode"]);
                }
                dataReader.Close();
                conn.Close();
            }            
            return user;
        }

        public async Task<User> GetUserWithPasswordAsync(string userName)
        {
            var user = new User();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);


            var commandText = string.Format("SELECT [Id] ,[UserName] ,[Password] ,[FirstName] ,[LastName] ," +
                "[PhoneNo] ,[Email] ,[UserTypeId],[CompanyId],[IsAdmin] ,[IsActive] ,[ActivationCode] FROM [User]  WITH(NOLOCK) WHERE UserName ='{0}' ", userName);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {

                    user.Id = Convert.ToInt32(dataReader["Id"]);
                    user.UserName = Convert.ToString(dataReader["UserName"]).ToLower();
                    user.Password = Convert.ToString(dataReader["Password"]).ToLower();
                    user.FirstName = Convert.ToString(dataReader["FirstName"]).ToLower();
                    user.Password = Convert.ToString(dataReader["Password"]).ToLower();
                    user.LastName = Convert.ToString(dataReader["LastName"]);
                    user.PhoneNo = Convert.ToString(dataReader["PhoneNo"]);
                    user.Email = Convert.ToString(dataReader["Email"]);
                    user.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    user.UserTypeId = Convert.ToInt32(dataReader["UserTypeId"]);
                    user.IsAdmin = Convert.ToBoolean(dataReader["IsAdmin"]);
                    user.IsActive = Convert.ToBoolean(dataReader["IsActive"]);
                    user.ActivationCode = Convert.ToString(dataReader["ActivationCode"]);
                }
                dataReader.Close();
                conn.Close();
            }

            return user;
        }

        public async Task<User> GeUserbyIdAsync(int userId)
        {
            return await Task.Run(() => GetAllUsersAsync().Result.Where(p => p.Id == userId).FirstOrDefault());
        }

        public async Task UpdateUserAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSettings.ConnectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction("SampleTransaction");

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                   
                    var sql = string.Format($"UPDATE [dbo].[User]   SET [UserName] = '{user.UserName}' ,[Password] = '{user.Password}' ,[FirstName] = '{user.FirstName}' ,[LastName] = '{user.LastName}' ,[Email] = '{user.Email}' ,[CompanyId] = '{user.CompanyId}' ,[UserTypeId] = '{user.UserTypeId}' ,[IsAdmin] = '{user.IsAdmin}',[IsActive] = '{user.IsActive}',[PhoneNo] = '{user.PhoneNo}' WHERE id = '{user.Id}'");
                    command.CommandText = sql;
                    await command.ExecuteNonQueryAsync();                   
                    // Attempt to commit the transaction.
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
