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
    public class NewsLetterRepository: INewsLetterRepository
    {
        private readonly IUserRepository userRepository;
        public NewsLetterRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<int> AddAsync(NewsLetterSubscription obj)
        {
            int id = 0;
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
                    string sql = string.Format($"INSERT INTO [dbo].[NewsLetterSubscription] ([CompanyId] ,[FirstName] ,[LastName],[Email] ,[IsActive]) VALUES ('{obj.CompanyId}','{obj.FirstName}','{obj.LastName}','{obj.Email}','{1}')");
                    sql = sql + " Select Scope_Identity()";
                    command.CommandText = sql;

                    var execid = await command.ExecuteScalarAsync();
                    id = Convert.ToInt32(execid.ToString());
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return id;
        }

        public async Task DeleteAsync(int id)
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
                    var sql = string.Format("DELETE FROM [dbo].[NewsLetterSubscription]  WHERE id = '{0}'", id);
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

        public async Task<IEnumerable<NewsLetterSubscription>> GetAllAsync(int companyId)
        {
            List<NewsLetterSubscription> orders = new List<NewsLetterSubscription>();

            var commandText = "";
            
            commandText = string.Format($"SELECT [Id] ,[CompanyId] ,[FirstName] ,[LastName] ,[Email] ,[IsActive]  FROM [dbo].[NewsLetterSubscription] where CompanyId = '{companyId}'  ");

            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var order = new NewsLetterSubscription();
                    order.Id = Convert.ToInt32(dataReader["Id"]);
                    order.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    order.FirstName = Convert.ToString(dataReader["FirstName"]);
                    order.LastName = Convert.ToString(dataReader["LastName"]);
                    order.Email = Convert.ToString(dataReader["Email"]);
                    order.IsActive = Convert.ToBoolean(dataReader["IsActive"]);

                    orders.Add(order);
                }
                dataReader.Close();
                conn.Close();
            }

            return orders;
        }
              
    }
}
