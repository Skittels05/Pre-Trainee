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

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.FindAll(false).ToListAsync();
        }

        public async Task<Author?> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
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
            var author = await _authorRepository.FindByCondition(a => a.Id == id).FirstOrDefaultAsync();
            if (author != null)
            {
                await _authorRepository.DeleteAsync(author);
            }
        }
        public async Task<List<Author>> GetAuthorsWithBookCountAsync(int bookCount)
        {
            return await _authorRepository.FindAll(false)
                .Where(a => a.Books.Count == bookCount)
                .ToListAsync();
        }


        public async Task<List<Author>> FindAuthorsByNameAsync(string namePart)
        {
            return await _authorRepository.FindByCondition(a => a.Name.Contains(namePart)).ToListAsync();
        }
    }
}
