using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDataAccess
{
    /// <summary>
    /// A wrapper class for making SQL calls in Dapper
    /// </summary>
    /// <typeparam name="TDbProvider">Database provider. For example, SQL Server, Oracle, MySQL, SQLite, etc.</typeparam>
    public sealed class DataAccess<TDbProvider> : IDataAccess, IDisposable  
        where TDbProvider : IDbConnection, new()
    {
        private readonly TDbProvider _connection;

        public DataAccess(string connectionString)
        {
            _connection = new TDbProvider()
            {
                ConnectionString = connectionString
            };
            _connection.Open();
        }
        /// <summary>
        /// Returns a collection of data from an SQL query.
        /// </summary>
        /// <typeparam name="TClass">The class the data will be mapped tp</typeparam>
        /// <typeparam name="TParameter">The parameter type; for example, dynamic, key value pair.</typeparam>
        /// <param name="storedProcedure">The stored procedure you want your SQL provider to execute</param>
        /// <param name="parameter">Parameters to be injected into the stored procedure</param>    
        public async Task<IEnumerable<TClass>> GetDataAsync<TClass, TParameter>(string storedProcedure, TParameter parameter)
        {
            IEnumerable<TClass> data = await _connection.QueryAsync<TClass>(storedProcedure, parameter, commandType: CommandType.StoredProcedure);
            return data;
        }
        /// <summary>
        /// Returns a single record from a SQL Query
        /// </summary>
        /// <typeparam name="TClass">The class the data will be mapped tp</typeparam>
        /// <typeparam name="TParameter">The parameter type; for example, dynamic, key value pair.</typeparam>
        /// <param name="storedProcedure">The stored procedure you want your SQL provider to execute</param>
        /// <param name="parameter">Parameters to be injected into the stored procedure</param>    
        public async Task<TClass> GetDataFirstOrDefaultAsync<TClass, TParameter>(string storedProcedure, TParameter parameter)
        {
            TClass data = await _connection.QueryFirstOrDefaultAsync<TClass>(storedProcedure, parameter, commandType: CommandType.StoredProcedure);
            return data;
        }
        /// <summary>
        /// Closes any connection
        /// </summary>
        public void Dispose()
        {
            _connection.Close();
        }
    }
}
