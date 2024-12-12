using Application.Books.Commands;
using Application.Books.Queries;
using Domain.Models;
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
                var operationResult = await _mediator.Send(command);

                if (operationResult.IsSuccessful)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
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
                var operationResult = await _mediator.Send(new DeleteBookCommand(id));

                if (operationResult.IsSuccessful)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
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
                var operationResult = await _mediator.Send(new GetAllBooksQuery());

                if (operationResult.IsSuccessful)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
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
                var operationResult = await _mediator.Send(new GetBookByIdQuery(id));

                if (operationResult.IsSuccessful)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
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
                var operationResult = await _mediator.Send(command);

                if (operationResult.IsSuccessful)
                {
                    return Ok(new { message = operationResult.Message, data = operationResult.Data });
                }
                else
                {
                    return BadRequest(new { message = operationResult.Message, errors = operationResult.ErrorMessage });
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while updating the book. {ex}");
            }
        }
    }
}
