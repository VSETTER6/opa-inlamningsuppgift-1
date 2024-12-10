using Application.Book.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Book.Handlers
{
    public class AddBookHandler : IRequestHandler<AddBookCommand, BookModel>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookModel> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                throw new ArgumentException("Title and description cannot be empty.");
            }

            BookModel newBook = new BookModel
            {
                Id = Guid.NewGuid(),
                Title = request.title,
                Description = request.description
            };

            await _bookRepository.AddBook(newBook);

            return newBook;
        }
    }
}
