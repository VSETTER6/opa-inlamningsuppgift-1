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
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public GetAuthorByIdHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }
        public Task<AuthorModel> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id <= 0)
            {
                throw new ArgumentException("ID must be a positive number.");
            }

            var authorId = _fakeDatabase.GetAuthorById(request.id);
            if (authorId == null)
            {
                throw new KeyNotFoundException($"No book found with ID = {request.id}");
            }

            return Task.FromResult(authorId);
        }
    }
}
