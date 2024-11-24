using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.Commands
{
    public record UpdateBookCommand(int id, string title, string description) : IRequest<BookModel>;
}
