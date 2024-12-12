using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public record GetBookByIdQuery(Guid id) : IRequest<Book>;
}
