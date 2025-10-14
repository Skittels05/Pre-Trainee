using Dapper;
using System.Data;
using TasksRepository;
using TasksFactory;

namespace TaskApp.DataAccess
{
    public class DbExecutorDapper : IDbExecutor
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbExecutorDapper(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        private IDbConnection CreateConnection() => _connectionFactory.CreateConnection();

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {
            using var conn = CreateConnection();
            return await conn.QueryAsync<T>(sql, parameters);
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null)
        {
            using var conn = CreateConnection();
            return await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, parameters);
        }

        public async Task<T> ExecuteScalarAsync<T>(string sql, object? parameters = null)
        {
            using var conn = CreateConnection();
            return await conn.ExecuteScalarAsync<T>(sql, parameters);
        }
    }

}
