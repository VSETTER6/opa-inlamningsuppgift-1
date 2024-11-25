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
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, AuthorModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public AddAuthorHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public async Task<AuthorModel> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                throw new ArgumentException("First name, last name and category cannot be empty.");
            }

            var newAuthor = new AuthorModel
            {
                Id = _fakeDatabase.GetAllAuthors().Count + 1,
                FirstName = request.firstName,
                LastName = request.lastName,
                Category = request.category
            };

            _fakeDatabase.AddAuthor(newAuthor);

            return newAuthor;
        }
    }
}
