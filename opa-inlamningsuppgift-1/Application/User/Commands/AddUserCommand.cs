using Application.User.DTOS;
using Domain.Models;
using MediatR;

namespace Application.User.Commands
{
    public record AddUserCommand(UserDto userDto) : IRequest<UserModel>;
}
