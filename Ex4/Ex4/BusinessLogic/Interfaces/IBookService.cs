using Ex4.BusinessLogic.DTO;
using Ex4.BusinessLogic.Models;

namespace Ex4.BusinessLogic.Interfaces
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto?> GetBookByIdAsync(int id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<List<BookDto>> GetBooksPublishedAfterAsync(int year);
    }
}
