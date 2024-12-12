using Application.Books.Commands;
using Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] AddBookCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while adding the book. {ex}");
            }
        }

        [Authorize]
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteBookCommand(id));

                return Ok($"Book has been successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while deleting the book. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await _mediator.Send(new GetAllBooksQuery());
                return Ok(books);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while getting the books. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetBookById")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var book = await _mediator.Send(new GetBookByIdQuery(id));
                return Ok(book);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while getting the book. {ex}");
            }
        }

        [Authorize]
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok($"Book with ID {id} has been successfully updated.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while updating the book. {ex}");
            }
        }
    }
}
