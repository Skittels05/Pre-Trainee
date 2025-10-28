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

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAll()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Author author)
        {
            await _authorService.AddAuthorAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Author author)
        {
            if (id != author.Id) return BadRequest();
            await _authorService.UpdateAuthorAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }

        
        [HttpGet("by-book-count/{count}")]
        public async Task<ActionResult<List<Author>>> GetAuthorsByBookCount(int count)
        {
            var authors = await _authorService.GetAuthorsWithBookCountAsync(count);
            return Ok(authors);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Author>>> Search([FromQuery] string name)
        {
            var authors = await _authorService.FindAuthorsByNameAsync(name);
            return Ok(authors);
        }
    }
}
