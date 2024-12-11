﻿using Application.Book.Commands;
using Application.Interfaces.RepositoryInterfaces;
using MediatR;

namespace Application.Book.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetBookById(request.id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.id} was not found.");
            }

            try
            {
                await _bookRepository.DeleteBook(request.id);

                return Unit.Value;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting the book. {ex}");
            }
        }
    }
}
