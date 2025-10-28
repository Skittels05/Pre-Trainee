using Ex4.BusinessLogic.Models;
using System.Linq;

namespace Ex4.DataAccess.Interfaces
{
    public interface IBookRepository
    {
        IQueryable<Book> FindAll(bool trackChanges = false);
        IQueryable<Book> FindByCondition(System.Linq.Expressions.Expression<Func<Book, bool>> expression, bool trackChanges = false);
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
