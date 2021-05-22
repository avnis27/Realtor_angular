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
    public class CompanyRepository : ICompanyRepository
    {
        public async Task<Company> GetAsync(int companyId)
        {
            var order = new Company();

            var commandText = string.Format($"SELECT [id] ,[Name] ,[Address] ,[PhoneNo] ,[FaxNo] ,[EMail],[ContactPersonName] FROM [dbo].[Company] where Id = '{companyId}'  ");

            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    
                    order.Id = Convert.ToInt32(dataReader["Id"]);
                    order.Name = Convert.ToString(dataReader["Name"]);
                    order.Address = Convert.ToString(dataReader["Address"]);
                    order.PhoneNo = Convert.ToString(dataReader["PhoneNo"]);
                    order.FaxNo = Convert.ToString(dataReader["FaxNo"]);
                    order.EMail = Convert.ToString(dataReader["EMail"]);
                    order.ContactPersonName = Convert.ToString(dataReader["ContactPersonName"]);                    
                }
                dataReader.Close();
                conn.Close();
            }

            return order;
        }
    }
}
