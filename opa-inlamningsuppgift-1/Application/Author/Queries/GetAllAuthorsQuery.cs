using Domain.Models;
using MediatR;

namespace Application.Author.Queries
{
    public record GetAllAuthorsQuery() : IRequest<List<Domain.Models.Author>>;   
}
