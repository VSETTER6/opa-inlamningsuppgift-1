using Application.Author.Commands;
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
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, AuthorModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public UpdateAuthorHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<AuthorModel> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorToUpdate = _fakeDatabase.GetAuthorById(request.id);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.id} not found.");
            }

            authorToUpdate.FirstName = request.firstName;
            authorToUpdate.LastName = request.lastName;
            authorToUpdate.Category = request.category;

            _fakeDatabase.UpdateAuthor(authorToUpdate);

            return Task.FromResult(authorToUpdate);
        }
    }
}
