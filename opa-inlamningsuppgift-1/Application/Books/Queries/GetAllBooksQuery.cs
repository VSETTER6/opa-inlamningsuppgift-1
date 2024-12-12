using Domain.Models;
using MediatR;

namespace Application.Books.Queries
{
    public record GetAllBooksQuery() : IRequest<List<Book>>;
}
