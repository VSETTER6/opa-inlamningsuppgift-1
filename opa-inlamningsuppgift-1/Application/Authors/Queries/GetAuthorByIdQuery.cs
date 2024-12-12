using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public record GetAuthorByIdQuery(Guid id) : IRequest<Author>;
}
