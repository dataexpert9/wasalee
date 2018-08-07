using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Wasalee.DatabaseExtensions
{
    public class CustomTypeSqlQuery<T> where T : class
    {
        private IMapper _mapper;

        public DatabaseFacade DatabaseFacade { get; set; }
        public string SQLQuery { get; set; }

        public CustomTypeSqlQuery()
        {
            _mapper = new MapperConfiguration(cfg => {
                //cfg.AddDataReaderMapping();
                cfg.CreateMap<IDataRecord, T>();
            }).CreateMapper();
        }

        public async Task<IList<T>> ToListAsync()
        {
            IList<T> results = new List<T>();
            var conn = DatabaseFacade.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = SQLQuery;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                        results = _mapper.Map<IDataReader, IEnumerable<T>>(reader)
                                         .ToList();
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        public async Task<T> FirstOrDefaultAsync()
        {
            T result = null;
            var conn = DatabaseFacade.GetDbConnection();
            try
            {
                await conn.OpenAsync();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = SQLQuery;
                    DbDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        var results = _mapper.Map<IDataReader, IEnumerable<T>>(reader)
                                             .ToList();
                        result = results.FirstOrDefault();
                    }
                    reader.Dispose();
                }
            }
            finally
            {
                conn.Close();
            }
            return result;
        }
    }
}
