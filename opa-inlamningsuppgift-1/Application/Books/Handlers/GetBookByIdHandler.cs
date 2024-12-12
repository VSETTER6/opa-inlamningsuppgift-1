using Application.Books.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Books.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Domain.Models.Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            Book book = await _bookRepository.GetBookById(request.id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} was not found.");
            }

            try
            {
                await _bookRepository.GetBookById(request.id);
                return book;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the book ID. {ex}");
            }
        }
    }
}
