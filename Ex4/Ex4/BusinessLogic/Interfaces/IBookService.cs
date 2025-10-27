using Ex4.BusinessLogic.Models;

namespace Ex4.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(int id);
        Book Add(Book book);
        bool Update(int id, Book book);
        bool Delete(int id);
    }
}
