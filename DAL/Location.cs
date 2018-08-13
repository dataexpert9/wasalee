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
using Component.ResponseFormats;

namespace DAL
{
    public class Location
    {

        public DataContext _dbContext { get; set; }

        public Location(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Distance(Location destination, int srid=4326)
        {
            var source = this;
            
            var Distance = string.Empty;
            var query = @"DECLARE @target geography =  geography::Point(" + destination.Latitude + @"," + destination.Longitude + @"," +srid+@")
                        DECLARE @orig geography = geography::Point(" + source.Latitude + @"," + source.Longitude + @"," + srid + @")
                        SELECT @orig.STDistance(@target) as Distance";
            try
            {
                var dbConn = _dbContext.Database.GetDbConnection();
                dbConn.Open();
                var command = dbConn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                return Convert.ToDouble(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                Error.LogError(ex);
                throw ex;
            }
        }

    }


}
