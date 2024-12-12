using Domain.Models;
using MediatR;

namespace Application.Authors.Commands
{
    public record AddAuthorCommand(string firstName, string lastName, string category) : IRequest<Author>;
}
