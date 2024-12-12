using Domain.Models;
using MediatR;

namespace Application.Authors.Queries
{
    public record GetAllAuthorsQuery() : IRequest<OperationResult<List<Author>>>;   
}
