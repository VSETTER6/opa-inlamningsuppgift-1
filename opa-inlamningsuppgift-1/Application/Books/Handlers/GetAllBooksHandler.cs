using Application.Books.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var books = await _bookRepository.GetAllBooks();
                return books;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the books. {ex}");
            }
        }
    }
}
