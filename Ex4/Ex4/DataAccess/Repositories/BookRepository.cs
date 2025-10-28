using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Interfaces;
using System.Xml.Linq;

namespace Ex4.DataAccess.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context) { }
    }
}
