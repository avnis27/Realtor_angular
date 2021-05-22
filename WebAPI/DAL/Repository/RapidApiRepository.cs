using DAL.DBHelper;
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
    public class RapidApiRepository : IRapidApiRepository
    {

        private readonly ISqlHelper _sqlHelper;
        

        public RapidApiRepository(ISqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;            
        }

        public async Task<RapidApiDetail> GetMasterAsync(int companyId)
        {
            var rapidApi = new RapidApiDetail();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);
            var commandText = string.Format($"SELECT  [Id]  ,[EMail] ,[x-rapidapi-key] ,[x-rapidapi-host] ,[CompanyId] FROM [dbo].[RapidApiDetail] WHERE CompanyId = '{companyId}'  ");

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    rapidApi.Id = Convert.ToInt32(dataReader["Id"]);
                    rapidApi.EMail = Convert.ToString(dataReader["EMail"]);
                    rapidApi.x_rapidapi_key = Convert.ToString(dataReader["x-rapidapi-key"]);
                    rapidApi.x_rapidapi_host = Convert.ToString(dataReader["x-rapidapi-host"]);
                    rapidApi.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);                
                    
                }
                dataReader.Close();
                conn.Close();
            }           
            conn.Close();
            return rapidApi;
        }

        public async Task<RapidApiDetail> GetLastRecordAsync()
        {
            var rapidApi = new RapidApiDetail();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);
            var commandText = string.Format($"SELECT top 1 [Id]  ,[EMail] ,[x-rapidapi-key] ,[x-rapidapi-host] ,[CompanyId] FROM [dbo].[RapidApiDetail] order by id desc  ");

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    rapidApi.Id = Convert.ToInt32(dataReader["Id"]);
                    rapidApi.EMail = Convert.ToString(dataReader["EMail"]);
                    rapidApi.x_rapidapi_key = Convert.ToString(dataReader["x-rapidapi-key"]);
                    rapidApi.x_rapidapi_host = Convert.ToString(dataReader["x-rapidapi-host"]);
                    rapidApi.CompanyId = Convert.ToInt32(dataReader["CompanyId"]);

                }
                dataReader.Close();
                conn.Close();
            }
            conn.Close();
            return rapidApi;
        }

        public async Task UpdateAsync(int currentId)
        {
            var lastRecord = await GetLastRecordAsync();
            int lastId = lastRecord.Id;
            
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
                    string sql = string.Format($"update [RapidApiDetail] set CompanyId = 2");                   
                    command.CommandText = sql;
                    await command.ExecuteNonQueryAsync();

                    int nextId = currentId + 1;
                    if (currentId == lastId)
                    {
                        nextId = 1;
                    }
                     sql = string.Format($"update [RapidApiDetail] set CompanyId = 1 where id = '{nextId}'");                    
                    command.CommandText = sql;
                    await command.ExecuteNonQueryAsync();
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
