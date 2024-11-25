using Application.Author.Commands;
using Application.Author.Queries;
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
    public class AuthorModelController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorModelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorModelController>
        [HttpGet]
        public async Task<List<AuthorModel>> Get()
        {
            return await _mediator.Send(new GetAllAuthorsQuery());
        }

        // GET api/<AuthorModelController>/5
        [HttpGet("{id}")]
        public async Task<AuthorModel> Get(int id)
        {
            return await _mediator.Send(new GetAuthorByIdQuery(id));
        }

        // POST api/<AuthorModelController>
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
            {
                return BadRequest("Failed to add author.");
            }

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/<AuthorModelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command)
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

        // DELETE api/<AuthorModelController>/5
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAuthor(int id)
        {
            return await _mediator.Send(new DeleteAuthorCommand(id));
        }
    }
}
