using Domain.Models;
using MediatR;

namespace Application.Book.Commands
{
    public record AddBookCommand(string title, string description) : IRequest<BookModel>;
}
