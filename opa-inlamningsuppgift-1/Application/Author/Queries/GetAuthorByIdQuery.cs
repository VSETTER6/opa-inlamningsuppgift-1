using Domain.Models;
using MediatR;

namespace Application.Author.Queries
{
    public record GetAuthorByIdQuery(Guid id) : IRequest<AuthorModel>;
}
