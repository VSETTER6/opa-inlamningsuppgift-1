using Application.Book.Commands;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public DeleteBookHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var books = _fakeDatabase.GetAllBooks();

            var bookToRemove = books.FirstOrDefault(book => book.Id == request.id);

            if (bookToRemove == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} not found.");   
            }

            _fakeDatabase.DeleteBook(request.id);

            return await Task.FromResult(true);
        }
    }
}
