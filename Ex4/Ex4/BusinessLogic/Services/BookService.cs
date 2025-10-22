using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Repositories;

namespace Ex4.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repo;
        private readonly IAuthorRepository _authorRepo;

        public BookService(IBookRepository repo, IAuthorRepository authorRepo)
        {
            _repo = repo;
            _authorRepo = authorRepo;
        }

        public IEnumerable<Book> GetAll() => _repo.GetAll();

        public Book? GetById(int id) => _repo.GetById(id);

        public Book Add(Book book)
        {
            ValidateBook(book);
            return _repo.Add(book);
        }

        public bool Update(int id, Book book)
        {
            ValidateBook(book);
            return _repo.Update(id, book);
        }

        public bool Delete(int id) => _repo.Delete(id);
        private void ValidateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Название книги обязательно для заполнения.");

            if (book.PublishedYear < 0 || book.PublishedYear > DateTime.Now.Year)
                throw new ArgumentException("Год публикации указан некорректно.");
            var author = _authorRepo.GetById(book.AuthorId);
            if (author == null)
                throw new ArgumentException($"Автор с ID {book.AuthorId} не найден. Укажите существующего автора.");
        }
    }
}
