using Ex4.BusinessLogic.DTO;
using Ex4.BusinessLogic.Interfaces;
using Ex4.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ex4.UserInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/authors
        [HttpGet]
        public async Task<ActionResult<List<AuthorDto>>> GetAllAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET: api/authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        // POST: api/authors
        [HttpPost]
        public async Task<ActionResult> AddAuthor([FromBody] Author author)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }

        // PUT: api/authors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(int id, [FromBody] Author author)
        {
            if (id != author.Id)
                return BadRequest("ID в пути и теле не совпадают.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _authorService.UpdateAuthorAsync(author);
            return NoContent();
        }

        // DELETE: api/authors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }

        // GET: api/authors/with-books/3
        [HttpGet("with-books/{bookCount}")]
        public async Task<ActionResult<List<AuthorDto>>> GetAuthorsWithBookCount(int bookCount)
        {
            var authors = await _authorService.GetAuthorsWithBookCountAsync(bookCount);
            return Ok(authors);
        }

        // GET: api/authors/search?namePart=Тол
        [HttpGet("search")]
        public async Task<ActionResult<List<AuthorDto>>> FindAuthorsByName([FromQuery] string namePart)
        {
            if (string.IsNullOrWhiteSpace(namePart))
                return BadRequest("Параметр namePart обязателен.");

            var authors = await _authorService.FindAuthorsByNameAsync(namePart);
            return Ok(authors);
        }
    }
}
