using Ex4.BusinessLogic.DTO;
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
            var book = await _bookRepository.FindByCondition(b => b.Id == id, true).FirstOrDefaultAsync();
            if (book != null)
                await _bookRepository.DeleteAsync(book);
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            return await _bookRepository.FindAll(false)
                .Include(b => b.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorName = b.Author.Name
                })
                .ToListAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.FindByCondition(b => b.Id == id, false)
                .Include(b => b.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorName = b.Author.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<BookDto>> GetBooksPublishedAfterAsync(int year)
        {
            return await _bookRepository.FindAll(false)
                .Include(b => b.Author)
                .Where(b => b.PublishedYear > year)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorName = b.Author.Name
                })
                .ToListAsync();
        }
    }
}
