using Application.Book.Commands;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;

namespace Application.Book.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllBooks();

            var bookToRemove = books.FirstOrDefault(book => book.Id == request.id);

            if (bookToRemove == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} not found.");   
            }

            await _bookRepository.DeleteBook(request.id);

            return true;
        }
    }
}
