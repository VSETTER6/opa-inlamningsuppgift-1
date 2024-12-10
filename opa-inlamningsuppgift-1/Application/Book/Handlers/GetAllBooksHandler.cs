using Application.Book.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Book.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookModel>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Task<List<BookModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return _bookRepository.GetAllBooks();
        }
    }
}
