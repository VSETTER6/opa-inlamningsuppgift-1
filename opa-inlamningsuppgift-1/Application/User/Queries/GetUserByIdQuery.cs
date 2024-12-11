using Domain.Models;
using MediatR;

namespace Application.User.Queries
{
    public record GetUserByIdQuery(Guid id) : IRequest<UserModel>;
}
