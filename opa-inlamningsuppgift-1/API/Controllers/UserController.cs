using Application.Users.Commands;
using Application.Users.DTOS;
using Application.Users.Queries;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            try
            {
                var operationResult = await _mediator.Send(new AddUserCommand(newUser));

                if (operationResult.Success)
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
                return BadRequest($"An error occurred while adding the user. {ex}");
            }
        }

        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new DeleteUserCommand(id));

                if (operationResult.Success)
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
                return BadRequest($"An error occurred while deleting the user. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var operationResult = await _mediator.Send(new GetAllUsersQuery());

                if (operationResult.Success)
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
                return BadRequest($"An error occurred while getting the users. {ex}");
            }
        }

        [Authorize]
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var operationResult = await _mediator.Send(new GetUserByIdQuery(id));

                if (operationResult.Success)
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
                return BadRequest($"An error occurred while getting the user. {ex}");
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> Login([FromBody] UserDto userWantingToLogIn)
        {
            return Ok(await _mediator.Send(new LoginUserQuery(userWantingToLogIn)));
        }

        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                var operationResult = await _mediator.Send(command);

                if (operationResult.Success)
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
                return BadRequest($"An error occurred while updating the user. {ex}");
            }
        }
    }
}
