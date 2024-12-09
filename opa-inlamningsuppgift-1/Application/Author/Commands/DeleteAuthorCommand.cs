using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Author.Commands
{
    public record DeleteAuthorCommand(Guid id) : IRequest<bool>;
}
