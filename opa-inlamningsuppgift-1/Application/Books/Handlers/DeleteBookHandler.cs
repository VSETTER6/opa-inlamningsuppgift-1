using Application.Books.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, OperationResult<Unit>>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Book book = await _bookRepository.GetBookById(request.id);

            if (book == null)
            {
                return OperationResult<Unit>.Failed($"Book with ID {request.id} was not found.");
            }
            else
            {
                await _bookRepository.DeleteBook(request.id);

                return OperationResult<Unit>.Successful(Unit.Value);
            }
        }
    }
}
