using Ex4.BusinessLogic.Models;

namespace Ex4.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<List<Book>> GetBooksPublishedAfterAsync(int year);
    }
}
