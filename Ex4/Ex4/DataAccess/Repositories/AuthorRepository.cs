using Ex4.BusinessLogic.Models;

namespace Ex4.DataAccess.Repositories
{
    public class AuthorRepository:IAuthorRepository
    {
        private readonly List<Author> _authors = new();
        private int _nextId = 1;

        public IEnumerable<Author> GetAll() => _authors;
        public Author? GetById(int id) => _authors.FirstOrDefault(a => a.Id == id);
        public Author Add(Author author)
        {
            author.Id = _nextId++;
            _authors.Add(author);
            return author;
        }
        public bool Update(int id, Author author)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = author.Name;
            existing.DateOfBirth = author.DateOfBirth;
            return true;
        }

        public bool Delete(int id)
        {
            var author = GetById(id);
            if (author == null) return false;
            _authors.Remove(author);
            return true;
        }
    }
}
