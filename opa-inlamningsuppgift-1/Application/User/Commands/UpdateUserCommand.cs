using Domain.Models;
using MediatR;

namespace Application.User.Commands
{
    public record UpdateUserCommand(Guid id, string username, string password) : IRequest<Domain.Models.User>;
}
