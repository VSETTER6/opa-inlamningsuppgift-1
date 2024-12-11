using Application.User.Commands;
using Application.User.DTOS;
using Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelController : ControllerBase
    {
        internal readonly IMediator _mediator;

        public UserModelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            try
            {
                var result = await _mediator.Send(new AddUserCommand(newUser));
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while adding the user. {ex}");
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand(id));

                return Ok($"User has been successfully deleted.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while deleting the user. {ex}");
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while getting the users. {ex}");
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(user);
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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                await _mediator.Send(command);

                return Ok($"User with ID {id} has been successfully updated.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"An error occurred while updating the user. {ex}");
            }
        }
    }
}
