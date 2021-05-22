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
    public class SavedListingRepository : ISavedListingRepository
    {
        private readonly IUserRepository userRepository;
        public SavedListingRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<int> AddAsync(SavedListing obj)
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
                    string sql = string.Format($"INSERT INTO [dbo].[SavedListing] ([CompanyId] ,[UserId]  ,[ReferenceNumber] ,[PropertyID]) VALUES ('{obj.CompanyId}','{obj.UserId}','{obj.ReferenceNumber}','{obj.PropertyID}')");
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
                    var sql = string.Format("DELETE FROM [dbo].[SavedListing]  WHERE id = '{0}'", id);
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

        public async Task<IEnumerable<SavedListing>> GetAllAsync(int companyId, int userId)
        {
            List<SavedListing> orders = new List<SavedListing>();

            var commandText = "";
            var userInfo = await userRepository.GeUserbyIdAsync(userId);
            if (userInfo.UserTypeId == 1)
            {
                commandText = string.Format($"SELECT [Id] ,[CompanyId],[UserId] ,[ReferenceNumber] ,[PropertyID]  FROM [dbo].[SavedListing] where CompanyId = '{companyId}'  ");
            }
            if (userInfo.UserTypeId == 2)
            {
                commandText = string.Format($"SELECT [Id] ,[CompanyId],[UserId] ,[ReferenceNumber] ,[PropertyID]  FROM [dbo].[SavedListing] where CompanyId = '{companyId}' AND UserId = '{userId}'  ");
            }            

            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var order = new SavedListing();
                    order.Id = Convert.ToInt32(dataReader["Id"]);
                    order.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    order.UserId = Convert.ToInt32(dataReader["UserId"]);
                    order.ReferenceNumber = Convert.ToString(dataReader["ReferenceNumber"]);
                    order.PropertyID = Convert.ToString(dataReader["PropertyID"]);
                    
                    orders.Add(order);
                }
                dataReader.Close();
                conn.Close();
            }        

            return orders;
        }       
    }
}
