using Domain.Models;
using MediatR;

namespace Application.Author.Commands
{
    public record AddAuthorCommand(string firstName, string lastName, string category) : IRequest<AuthorModel>;
}
