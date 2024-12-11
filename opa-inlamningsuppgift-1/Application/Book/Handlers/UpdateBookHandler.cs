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
                throw new KeyNotFoundException($"Book with ID {request.id} was not found.");
            }

            if (string.IsNullOrWhiteSpace(request.title) || string.IsNullOrWhiteSpace(request.description))
            {
                throw new ArgumentException("None of title or description can be empty.");
            }

            try
            {
                bookToUpdate.Title = request.title;
                bookToUpdate.Description = request.description;

                await _bookRepository.UpdateBook(bookToUpdate.Id, bookToUpdate);

                return bookToUpdate;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the book. {ex}");
            }
        }
    }
}
