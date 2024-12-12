using Application.Books.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, OperationResult<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationResult<Book>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Book book = await _bookRepository.GetBookById(request.id);

            if (book == null)
            {
                return OperationResult<Book>.Failed($"Book with ID {request.id} was not found.");
            }
            else
            {
                await _bookRepository.GetBookById(request.id);
                return OperationResult<Book>.Successful(book);
            }
        }
    }
}
