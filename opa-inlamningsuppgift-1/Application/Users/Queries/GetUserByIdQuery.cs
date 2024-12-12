using Domain.Models;
using MediatR;

namespace Application.Users.Queries
{
    public record GetUserByIdQuery(Guid id) : IRequest<OperationResult<User>>;
}
