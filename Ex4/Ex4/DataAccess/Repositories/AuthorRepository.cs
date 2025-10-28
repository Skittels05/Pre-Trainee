using Ex4.BusinessLogic.Models;
using Ex4.DataAccess.Interfaces;
using System.Linq.Expressions;

namespace Ex4.DataAccess.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context) { }
    }
}
