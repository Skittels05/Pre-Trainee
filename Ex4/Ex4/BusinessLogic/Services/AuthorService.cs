using Ex4.BusinessLogic.DTO;
using Ex4.BusinessLogic.Interfaces;
using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ex4.BusinessLogic.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task AddAuthorAsync(Author author)
        {
            await _authorRepository.CreateAsync(author);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            await _authorRepository.UpdateAsync(author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.FindByCondition(a => a.Id == id, true).FirstOrDefaultAsync();
            if (author != null)
                await _authorRepository.DeleteAsync(author);
        }

        public async Task<List<AuthorDto>> GetAllAuthorsAsync()
        {
            return await _authorRepository.FindAll(false)
                .Include(a => a.Books)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        PublishedYear = b.PublishedYear,
                        AuthorName = a.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.FindByCondition(a => a.Id == id, false)
                .Include(a => a.Books)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        PublishedYear = b.PublishedYear,
                        AuthorName = a.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AuthorDto>> GetAuthorsWithBookCountAsync(int bookCount)
        {
            return await _authorRepository.FindAll(false)
                .Include(a => a.Books)
                .Where(a => a.Books.Count == bookCount)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        PublishedYear = b.PublishedYear,
                        AuthorName = a.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<List<AuthorDto>> FindAuthorsByNameAsync(string namePart)
        {
            return await _authorRepository.FindByCondition(
                    a => a.Name.Contains(namePart) || a.Name.StartsWith(namePart), false)
                .Include(a => a.Books)
                .Select(a => new AuthorDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Books = a.Books.Select(b => new BookDto
                    {
                        Id = b.Id,
                        Title = b.Title,
                        PublishedYear = b.PublishedYear,
                        AuthorName = a.Name
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
