using Application.Book.Commands;
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
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public UpdateBookHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<BookModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _fakeDatabase.GetBookById(request.id);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} not found.");
            }

            bookToUpdate.Title = request.title;
            bookToUpdate.Description = request.description;

            _fakeDatabase.UpdateBook(bookToUpdate);

            return Task.FromResult(bookToUpdate);
        }
    }
}
