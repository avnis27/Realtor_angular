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
    public class LocationRepository : ILocationRepository
    {
        private readonly ISqlHelper _sqlHelper;


        public LocationRepository(ISqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public async Task<IEnumerable<Location>> GetMasterAsync()
        {
            var objs = new List<Location>();
            SqlConnection conn = new SqlConnection(ConnectionSettings.ConnectionString);
            var commandText = string.Format($"SELECT  [Id] ,[Name] ,[LatitudeMin] ,[LatitudeMax] ,[LongitudeMin]  ,[LongitudeMax]  ,[Latitude]  ,[Longitude] FROM [dbo].[Loation] ");

            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                cmd.CommandType = CommandType.Text;

                conn.Open();

                var dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    var obj = new Location();
                    obj.Id = Convert.ToInt32(dataReader["Id"]);
                    obj.Name = Convert.ToString(dataReader["Name"]);
                    obj.LatitudeMin = Convert.ToString(dataReader["LatitudeMin"]);
                    obj.LatitudeMax = Convert.ToString(dataReader["LatitudeMax"]);
                    obj.LongitudeMin = Convert.ToString(dataReader["LongitudeMin"]);
                    obj.LongitudeMax = Convert.ToString(dataReader["LongitudeMax"]);
                    obj.Latitude = Convert.ToString(dataReader["Latitude"]);
                    obj.Longitude = Convert.ToString(dataReader["Longitude"]);

                    objs.Add(obj);
                }
                dataReader.Close();
                conn.Close();
            }
            conn.Close();
            return objs;
        }
    }
}
