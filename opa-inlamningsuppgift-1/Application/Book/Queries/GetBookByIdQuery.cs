using Domain.Models;
using MediatR;

namespace Application.Book.Queries
{
    public record GetBookByIdQuery(Guid id) : IRequest<Domain.Models.Book>;
}
