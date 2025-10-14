using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksRepository
{
    public interface IDbExecutor
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null);
        Task<int> ExecuteAsync(string sql, object? parameters = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null);
    }
}
