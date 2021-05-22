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
    public class FeaturedListingRepository: IFeaturedListingRepository
    {
        private readonly IUserRepository userRepository;
        public FeaturedListingRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<int> AddAsync(FeaturedListing obj)
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
                    string sql = string.Format($"INSERT INTO [dbo].[FeaturedListing] ([CompanyId]  ,[ReferenceNumber] ,[PropertyID]) VALUES ('{obj.CompanyId}','{obj.ReferenceNumber}','{obj.PropertyID}')");
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
                    var sql = string.Format("DELETE FROM [dbo].[FeaturedListing]  WHERE id = '{0}'", id);
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

        public async Task<IEnumerable<FeaturedListing>> GetAllAsync(int companyId)
        {
            List<FeaturedListing> orders = new List<FeaturedListing>();

            var commandText = "";
            
            commandText = string.Format($"SELECT [Id] ,[CompanyId] ,[ReferenceNumber] ,[PropertyID]  FROM [dbo].[FeaturedListing] where CompanyId = '{companyId}'  ");

            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var order = new FeaturedListing();
                    order.Id = Convert.ToInt32(dataReader["Id"]);
                    order.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);                    
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
