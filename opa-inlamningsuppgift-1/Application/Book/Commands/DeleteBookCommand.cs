using MediatR;

namespace Application.Book.Commands
{
    public record DeleteBookCommand(Guid id) : IRequest<Unit>;
}
