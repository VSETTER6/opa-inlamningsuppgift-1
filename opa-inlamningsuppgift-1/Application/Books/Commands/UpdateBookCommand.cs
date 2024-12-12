using Domain.Models;
using MediatR;

namespace Application.Books.Commands
{
    public record UpdateBookCommand(Guid id, string title, string description) : IRequest<OperationResult<Book>>;
}
