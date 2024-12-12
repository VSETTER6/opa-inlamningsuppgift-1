using Application.Authors.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, Unit>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            Domain.Models.Author author = await _authorRepository.GetAuthorById(request.id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.id} was not found.");
            }

            try
            {
                await _authorRepository.DeleteAuthor(request.id);
                return Unit.Value;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting the author. {ex}");
            }
        }
    }
}
