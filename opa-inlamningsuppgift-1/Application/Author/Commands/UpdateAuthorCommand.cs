using Domain.Models;
using MediatR;

namespace Application.Author.Commands
{
    public record UpdateAuthorCommand(Guid id, string firstName, string lastName, string category) : IRequest<AuthorModel>;
}
