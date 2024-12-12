using Domain.Models;
using MediatR;

namespace Application.Books.Commands
{
    public record AddBookCommand(string title, string description) : IRequest<Book>;
}
