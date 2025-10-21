using Ex4.BusinessLogic.Models;
using Ex4.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ex4.UserInterface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var author = _service.GetById(id);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Author author)
        {
            try
            {
                var created = _service.Add(author);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Author author)
        {
            try
            {
                if (_service.Update(id, author))
                    return NoContent();
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_service.Delete(id))
                return NoContent();
            return NotFound();
        }
    }
}
