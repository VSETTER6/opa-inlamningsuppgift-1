﻿using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Author.Queries
{
    public record GetAuthorByIdQuery(Guid id) : IRequest<AuthorModel>;
}
