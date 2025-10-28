using Ex4.BusinessLogic.Models;
using System.Linq;

namespace Ex4.DataAccess.Interfaces
{
    public interface IAuthorRepository
    {
        IQueryable<Author> FindAll(bool trackChanges = false);
        IQueryable<Author> FindByCondition(System.Linq.Expressions.Expression<Func<Author, bool>> expression, bool trackChanges = false);
        Task CreateAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
    }
}
