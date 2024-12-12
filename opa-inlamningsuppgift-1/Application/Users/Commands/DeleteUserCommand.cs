using Domain.Models;
using MediatR;

namespace Application.Users.Commands
{
    public record DeleteUserCommand(Guid id) : IRequest<OperationResult<Unit>>;
}
