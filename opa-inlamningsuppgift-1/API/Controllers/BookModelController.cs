using Application.Author.Commands;
using Application.Author.Queries;
using Application.Book.Commands;
using Application.Book.Handlers;
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
    }
}
