using Application.Book.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Book.Handlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BookModel>
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookModel> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            BookModel bookToUpdate = await _bookRepository.GetBookById(request.id);

            if (bookToUpdate == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} not found.");
            }

            bookToUpdate.Title = request.title;
            bookToUpdate.Description = request.description;

            await _bookRepository.UpdateBook(bookToUpdate.Id ,bookToUpdate);

            return bookToUpdate;
        }
    }
}
