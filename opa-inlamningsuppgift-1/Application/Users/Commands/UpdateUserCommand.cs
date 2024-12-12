using Domain.Models;
using MediatR;

namespace Application.Users.Commands
{
    public record UpdateUserCommand(Guid id, string username, string password) : IRequest<OperationResult<User>>;
}
