using Ex4.BusinessLogic.Interfaces;
using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ex4.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.FindAll(false).ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.CreateAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.FindByCondition(b => b.Id == id).FirstOrDefaultAsync();
            if (book != null)
            {
                await _bookRepository.DeleteAsync(book);
            }
        }
        public async Task<List<Book>> GetBooksPublishedAfterAsync(int year)
        {
            return await _bookRepository.FindByCondition(b => b.PublishedYear > year)
                                        .ToListAsync();
        }
    }
}
