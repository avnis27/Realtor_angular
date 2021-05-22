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
    public class AgentRepository : IAgentRepository
    {
        public async Task UpdateAsync(Agent obj)
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

                    var sql = string.Format($"UPDATE [dbo].[Agent]   SET [FirstName] = '{obj.FirstName}' ,[LastName] = '{obj.LastName}' ,[Title] = '{obj.Title}' ,[Email] = '{obj.Email}' ,[PhoneNo] = '{obj.PhoneNo}',[ImagePath] = '{obj.ImagePath}'   WHERE id = '{obj.Id}'");
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

        public async Task<Agent> GetAllAsync(int companyId)
        {
            var order = new Agent();

            var commandText = "";

            commandText = string.Format($"SELECT  [Id]  ,[CompanyId]  ,[FirstName]  ,[LastName] ,[Title] ,[Email]  ,[PhoneNo]   ,[ImagePath] FROM [dbo].[Agent] where CompanyId = '{companyId}'  ");

            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {                    
                    order.Id = Convert.ToInt32(dataReader["Id"]);
                    order.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);
                    order.FirstName = Convert.ToString(dataReader["FirstName"]);
                    order.LastName = Convert.ToString(dataReader["LastName"]);
                    order.PhoneNo = Convert.ToString(dataReader["PhoneNo"]);
                    order.Email = Convert.ToString(dataReader["Email"]);
                    order.Title = Convert.ToString(dataReader["Title"]);
                    order.ImagePath = Convert.ToString(dataReader["ImagePath"]);            
                }
                dataReader.Close();
                conn.Close();
            }

            return order;
        }
    }
}
