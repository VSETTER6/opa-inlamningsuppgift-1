using Application.Book.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Book.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookModel>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<BookModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
            {
                throw new ArgumentException("ID can not be empty.");
            }

            var bookId = _bookRepository.GetBookById(request.id);
            if (bookId == null)
            {
                throw new KeyNotFoundException($"No book found with ID {request.id}.");
            }

            return bookId;
        }
    }
}
