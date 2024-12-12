using Domain.Models;
using MediatR;

namespace Application.User.Queries
{
    public record GetAllUsersQuery : IRequest<List<Domain.Models.User>>;
}
