using Application.Authors.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            Author author = await _authorRepository.GetAuthorById(request.id);

            if (author == null)
            {
                return OperationResult<Author>.Failed($"Author with ID {request.id} was not found.");
            }
            else
            {
                await _authorRepository.GetAuthorById(request.id);
                return OperationResult<Author>.Successful(author);
            }
        }
    }
}
