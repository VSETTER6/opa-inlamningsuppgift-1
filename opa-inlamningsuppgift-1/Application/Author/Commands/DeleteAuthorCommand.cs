using MediatR;

namespace Application.Author.Commands
{
    public record DeleteAuthorCommand(Guid id) : IRequest<Unit>;
}
