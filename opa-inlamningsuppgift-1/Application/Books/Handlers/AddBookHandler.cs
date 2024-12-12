using Application.Books.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class AddBookHandler : IRequestHandler<AddBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public AddBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                return OperationResult<Book>.Failed("None of title or description can be empty.");
            }
            else
            {
                Book newBook = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = request.title,
                    Description = request.description
                };

                await _bookRepository.AddBook(newBook);
                return OperationResult<Book>.Successful(newBook);
            }
        }
    }
}
