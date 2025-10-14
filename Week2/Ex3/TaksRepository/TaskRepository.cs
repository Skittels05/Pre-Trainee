using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksRepository
{
    public class TaskRepository : IGenericRepository<TaskItem>
    {
        private readonly IDbExecutor _db;
        public TaskRepository(IDbExecutor dbExecutor)
        {
            _db = dbExecutor;
        }

        public Task<int> AddAsync(TaskItem task)
        {
            var sql = @"
        INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt)
        VALUES (@Title, @Description, @IsCompleted, @CreatedAt);
        SELECT CAST(SCOPE_IDENTITY() AS int);";
            return _db.ExecuteScalarAsync<int>(sql, task);
        }

        public Task<bool> DeleteAsync(int id)
        {

            return ExecuteBoolAsync("DELETE FROM Tasks WHERE Id=@Id", new { Id = id });
        }

        public Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return _db.QueryAsync<TaskItem>("SELECT * FROM Tasks");
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            return _db.QueryFirstOrDefaultAsync<TaskItem>("SELECT * FROM Tasks WHERE Id=@Id", new { Id = id });
        }

        public Task<bool> UpdateAsync(TaskItem task)
        {
            var sql = "UPDATE Tasks SET Title=@Title,Description=@Description,IsCompleted=@IsCompleted WHERE Id=@Id";
            return ExecuteBoolAsync(sql, task);
        }
        private async Task<bool> ExecuteBoolAsync(string sql, object parameters)
        {
            var affected = await _db.ExecuteAsync(sql, parameters);
            return affected > 0;
        }
    }
}
