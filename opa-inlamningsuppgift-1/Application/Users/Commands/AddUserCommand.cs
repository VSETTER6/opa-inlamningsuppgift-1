using Application.Users.DTOS;
using Domain.Models;
using MediatR;

namespace Application.Users.Commands
{
    public record AddUserCommand(UserDto userDto) : IRequest<User>;
}
