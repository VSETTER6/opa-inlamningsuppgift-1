using MediatR;

namespace Application.Users.Commands
{
    public record DeleteUserCommand(Guid id) : IRequest<Unit>;
}
