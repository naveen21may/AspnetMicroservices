using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Discount.Data
{
    public class DapperContext : IDapperContext<DapperContext>
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public DapperContext(IDbConnection connection)
        {
            _connection = connection;
        }
        public void BeginTransaction()
        {
            _connection.Open();
           _transaction =  _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public async Task<int> ExecuteAsync(string sql, object instance)
        {
            return await _connection.ExecuteAsync(sql, instance);
            
        }

        public async Task<T> QueryFirstAsync<T>(string sql, object filter = null)
        {
            return await _connection.QueryFirstAsync<T>(sql, filter);   //IPhone X
        }

        
    }
}
