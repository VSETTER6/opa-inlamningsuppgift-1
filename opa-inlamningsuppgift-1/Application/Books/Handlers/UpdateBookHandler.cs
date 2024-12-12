using Application.Books.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            Book bookToUpdate = await _bookRepository.GetBookById(request.id);

            if (bookToUpdate == null)
            {
                return OperationResult<Book>.Failed($"Book with ID {request.id} was not found.");
            }
            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                return OperationResult<Book>.Failed("None of title or description can be empty.");
            }
            else
            {
                bookToUpdate.Title = request.title;
                bookToUpdate.Description = request.description;

                await _bookRepository.UpdateBook(bookToUpdate.Id, bookToUpdate);

                return OperationResult<Book>.Successful(bookToUpdate);
            }
        }
    }
}
