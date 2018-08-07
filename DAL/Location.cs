using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.DatabaseExtensions;

namespace DAL
{
    public class Location
    {
        //public int Id { get; set; }

        public DataContext _dbContext { get; set; }

        public Location(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public static Task UpdateLocation(DbContext ctx, string table, Location location, int id)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            string query = String.Format(@"UPDATE [dbo].[{0}] SET Location = geography::STPointFromText('POINT(' + CAST({1} AS VARCHAR(20)) + ' ' + CAST({2} AS VARCHAR(20)) + ')', 4326) WHERE(ID = {3})"
            , table.ToLower(), location.Longitude, location.Latitude, id);
            return  ctx.Database.ExecuteSqlCommandAsync(query);
        }

        //public async Task<Location> GetLocation(DbContext ctx, string table, int id)
        //{
        //    Location location = new Location();

        //    using (var command = ctx.Database.GetDbConnection().CreateCommand())
        //    {
        //        string query = String.Format("SELECT Location.Latitude, Location.Longitude FROM [dbo].[{0}] WHERE Id = {1}"
        //            , table, id);
        //        command.CommandText = query;
        //        ctx.Database.OpenConnection();
        //        using (var result = command.ExecuteReader())
        //        {
        //            if (result.HasRows)
        //            {
        //                while (await result.ReadAsync())
        //                {
        //                    location.Latitude = result.GetDouble(0);
        //                    location.Longitude = result.GetDouble(1);
        //                }
        //            }
        //        }
        //    }
        //    return location;
        //}

        public double Distance(Location location)
        {
            var Distance = string.Empty;
            var query = @"DECLARE @target geography = geography::Point(-35.23555, 56.6545, 4326)
                        DECLARE @orig geography = geography::Point("+location.Latitude+@","+location.Longitude+@", 4326)
                        SELECT @orig.STDistance(@target) as Distance";
            //var dDistance = _dbContext.Database.ExecuteSqlCommandAsync(query);

            try
            {
                //var employees = _dbContext.Store.FromSql(query).ToList();
                var dbConn = _dbContext.Database.GetDbConnection();
                dbConn.Open();
                var command = dbConn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                return Convert.ToDouble(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}
