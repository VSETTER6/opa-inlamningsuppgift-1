using MediatR;

namespace Application.User.Commands
{
    public record DeleteUserCommand(Guid id) : IRequest<Unit>;
}
