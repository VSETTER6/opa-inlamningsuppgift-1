using Application.Author.Commands;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Author.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public DeleteAuthorHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authors = _fakeDatabase.GetAllAuthors();

            var authorToRemove = authors.FirstOrDefault(author => author.Id == request.id);

            if (authorToRemove == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.id} not found.");
            }

            _fakeDatabase.DeleteAuthor(request.id);

            return await Task.FromResult(true);
        }
    }
}
