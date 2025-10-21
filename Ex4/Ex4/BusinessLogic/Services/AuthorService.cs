using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Repositories;

namespace Ex4.BusinessLogic.Services
{
    public class AuthorService:IAuthorService
    {
        private readonly IAuthorRepository _repo;

        public AuthorService(IAuthorRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Author> GetAll() => _repo.GetAll();

        public Author? GetById(int id) => _repo.GetById(id);

        public Author Add(Author author)
        {
            ValidateAuthor(author);
            return _repo.Add(author);
        }

        public bool Update(int id, Author author)
        {
            ValidateAuthor(author);
            return _repo.Update(id, author);
        }

        public bool Delete(int id) => _repo.Delete(id);
        private void ValidateAuthor(Author author)
        {
            if (string.IsNullOrWhiteSpace(author.Name))
                throw new ArgumentException("Имя автора обязательно для заполнения.");

            if (author.DateOfBirth > DateTime.Now)
                throw new ArgumentException("Дата рождения не может быть в будущем.");
        }
    }
}
