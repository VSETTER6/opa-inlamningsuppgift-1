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
    public class AddBookHandler : IRequestHandler<AddBookCommand, BookModel>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public AddBookHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public async Task<BookModel> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                throw new ArgumentException("Title and description cannot be empty.");
            }

            var newBook = new BookModel
            {
                Id = _fakeDatabase.GetAllBooks().Count + 1,
                Title = request.title,
                Description = request.description
            };

            // Lägg till boken i databasen
            _fakeDatabase.AddBook(newBook);

            return newBook;
        }
    }
}
