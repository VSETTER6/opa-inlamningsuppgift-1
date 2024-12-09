using Application.Book.Queries;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public GetBookByIdHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<BookModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
            {
                throw new ArgumentException("ID must be a valid GUID.");
            }

            var bookId = _fakeDatabase.GetBookById(request.id);
            if (bookId == null)
            {
                throw new KeyNotFoundException($"No book found with ID {request.id}.");
            }

            return Task.FromResult(bookId);
        }
    }
}
