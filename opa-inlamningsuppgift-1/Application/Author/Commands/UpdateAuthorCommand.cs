using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Author.Commands
{
    public record UpdateAuthorCommand(int id, string firstName, string lastName, string category) : IRequest<AuthorModel>;
}
