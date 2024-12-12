using Application.Book.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Book.Handlers
{
    public class AddBookHandler : IRequestHandler<AddBookCommand, Domain.Models.Book>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Domain.Models.Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                throw new ArgumentException("None of title or description can be empty.");
            }

            try
            {
                Domain.Models.Book newBook = new Domain.Models.Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.title,
                    Description = request.description
                };

                await _bookRepository.AddBook(newBook);
                return newBook;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while adding the book. {ex}");
            }
        }
    }
}
