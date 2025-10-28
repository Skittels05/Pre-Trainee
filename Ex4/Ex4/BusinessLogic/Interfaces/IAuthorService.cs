using Ex4.BusinessLogic.DTO;
using Ex4.BusinessLogic.Models;

namespace Ex4.BusinessLogic.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAuthorsAsync();
        Task<AuthorDto?> GetAuthorByIdAsync(int id);
        Task AddAuthorAsync(Author author);           
        Task UpdateAuthorAsync(Author author);
        Task DeleteAuthorAsync(int id);
        Task<List<AuthorDto>> GetAuthorsWithBookCountAsync(int bookCount);
        Task<List<AuthorDto>> FindAuthorsByNameAsync(string namePart);
    }
}
