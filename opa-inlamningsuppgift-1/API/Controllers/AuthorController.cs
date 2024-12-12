using Application.Authors.Commands;
using Application.Authors.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AddAuthorCommand command)
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
                return BadRequest($"An error occurred while adding the author. {ex}");
            }
        }

        [Authorize]
        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteAuthorCommand(id));

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
                return BadRequest($"An error occurred while deleting the author. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAllAuthorsQuery());

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
                return BadRequest($"An error occurred while getting the authors. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAuthorByIdQuery(id));

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
                return BadRequest($"An error occurred while getting the author. {ex}");
            }
        }

        [Authorize]
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> UpdateAuthor(Guid id, [FromBody] UpdateAuthorCommand command)
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
                return BadRequest($"An error occurred while updating the author. {ex}");
            }
        }
    }
}
