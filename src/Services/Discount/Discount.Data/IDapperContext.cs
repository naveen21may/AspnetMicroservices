using System;
using System.Threading.Tasks;

namespace Discount.Data
{
    public interface IDapperContext<T> where T:class
    {
        void BeginTransaction();
        void Commit();
        void Rollback();

        void Dispose();

        Task<T> QueryFirstAsync<T>(string sql, object filter = null);
        Task<int> ExecuteAsync(string sql, object instance);
    }
}
