using Ex4.BusinessLogic.DTO;
using Ex4.BusinessLogic.Interfaces;
using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Ex4.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IValidator<Book> _bookValidator;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IValidator<Book> bookValidator)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _bookValidator = bookValidator;
        }

        public async Task AddBookAsync(Book book)
        {
            await ValidateBookAsync(book);
            await ValidateAuthorExistsAsync(book.AuthorId);
            await ValidatePublicationYearAsync(book.AuthorId, book.PublishedYear);

            await _bookRepository.CreateAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await ValidateBookAsync(book);
            await ValidateAuthorExistsAsync(book.AuthorId);
            await ValidatePublicationYearAsync(book.AuthorId, book.PublishedYear);

            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository
                .FindByCondition(b => b.Id == id, true)
                .FirstOrDefaultAsync();

            if (book != null)
                await _bookRepository.DeleteAsync(book);
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            return await MapBooksToDto(_bookRepository.FindAll(false))
                .ToListAsync();
        }

        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            return await MapBooksToDto(
                    _bookRepository.FindByCondition(b => b.Id == id, false)
                )
                .FirstOrDefaultAsync();
        }

        public async Task<List<BookDto>> GetBooksPublishedAfterAsync(int year)
        {
            return await MapBooksToDto(
                    _bookRepository.FindAll(false)
                        .Where(b => b.PublishedYear > year)
                )
                .ToListAsync();
        }

        private async Task ValidateBookAsync(Book book)
        {
            var result = await _bookValidator.ValidateAsync(book);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }

        private async Task ValidateAuthorExistsAsync(int authorId)
        {
            var exists = await _authorRepository
                .FindByCondition(a => a.Id == authorId, false)
                .AnyAsync();

            if (!exists)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("AuthorId", "Указанный автор не существует.")
                });
        }

        private async Task ValidatePublicationYearAsync(int authorId, int publishedYear)
        {
            var authorBirthYear = await _authorRepository
                .FindByCondition(a => a.Id == authorId, false)
                .Select(a => a.DateOfBirth.Year)
                .FirstOrDefaultAsync();

            if (publishedYear < authorBirthYear)
                throw new ValidationException(new[]
                {
                    new ValidationFailure("PublishedYear",
                        "Книга не может быть опубликована раньше года рождения автора.")
                });
        }

        private IQueryable<BookDto> MapBooksToDto(IQueryable<Book> query)
        {
            return query
                .Include(b => b.Author)
                .Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    PublishedYear = b.PublishedYear,
                    AuthorName = b.Author.Name
                });
        }
    }
}