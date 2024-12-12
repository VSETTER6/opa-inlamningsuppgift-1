using Domain.Models;
using MediatR;

namespace Application.Book.Commands
{
    public record UpdateBookCommand(Guid id, string title, string description) : IRequest<Domain.Models.Book>;
}
