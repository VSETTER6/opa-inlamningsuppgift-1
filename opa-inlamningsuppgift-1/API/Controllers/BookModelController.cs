using Application.Book.Commands;
using Application.Book.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookModelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookModelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BookModelController>
        [HttpGet]
        public async Task<List<BookModel>> Get()
        {
            return await _mediator.Send(new GetAllBooksQuery());
        }

        // GET api/<BookModelController>/5
        [HttpGet("{id}")]
        public async Task<BookModel> Get(Guid id)
        {
            return await _mediator.Send(new GetBookByIdQuery(id));
        }

        // POST api/<BookModelController>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Failed to add book.");
            }

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }


        // PUT api/<BookModelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command)
        {
            if (id != command.id)
            {
                return BadRequest("ID does not match.");
            }

            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BookModelController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteBook(Guid id)
        {
            return await _mediator.Send(new DeleteBookCommand(id));
        }
    }
}
