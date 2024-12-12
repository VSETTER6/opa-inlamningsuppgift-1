using Domain.Models;
using MediatR;

namespace Application.Book.Queries
{
    public record GetAllBooksQuery() : IRequest<List<Domain.Models.Book>>;
}
