using Application.Author.Queries;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Author.Handlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, List<AuthorModel>>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public GetAllAuthorsHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<AuthorModel>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeDatabase.GetAllAuthors());
        }
    }
}
