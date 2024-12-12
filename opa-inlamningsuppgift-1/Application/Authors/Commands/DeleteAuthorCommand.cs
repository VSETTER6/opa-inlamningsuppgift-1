using Domain.Models;
using MediatR;

namespace Application.Authors.Commands
{
    public record DeleteAuthorCommand(Guid id) : IRequest<OperationResult<Unit>>;
}
