using Application.Authors.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, OperationResult<Unit>>
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Unit>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            Author author = await _authorRepository.GetAuthorById(request.id);

            if (author == null)
            {
                return OperationResult<Unit>.Failed($"Author with ID {request.id} was not found.");
            }
            else
            {
                await _authorRepository.DeleteAuthor(request.id);
                return OperationResult<Unit>.Successful(Unit.Value);
            }
        }
    }
}
