using Domain.Models;
using MediatR;

namespace Application.Authors.Commands
{
    public record UpdateAuthorCommand(Guid id, string firstName, string lastName, string category) : IRequest<Author>;
}
