using Ex4.BusinessLogic.Models;

namespace Ex4.BusinessLogic.Services
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author? GetById(int id);
        Author Add(Author author);
        bool Update(int id, Author author);
        bool Delete(int id);
    }
}
