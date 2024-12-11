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

        public async Task<BookModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookById(request.id);

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
