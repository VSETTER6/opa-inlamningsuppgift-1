using Application.User.Commands;
using Application.User.DTOS;
using Application.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto newUser)
        {
            return Ok(await _mediator.Send(new AddUserCommand(newUser)));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto userWantingToLogIn)
        {
            return Ok(await _mediator.Send(new LoginUserQuery(userWantingToLogIn)));
        }
    }
}
