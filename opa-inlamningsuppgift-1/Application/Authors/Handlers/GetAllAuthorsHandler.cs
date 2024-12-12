using Application.Authors.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace Application.Authors.Handlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, OperationResult<List<Author>>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<List<Author>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetAllAuthors();

            if (authors == null)
            {
                return OperationResult<List<Author>>.Failed("An error occurred while getting the authors.");
            }
            else
            {
                return OperationResult<List<Author>>.Successful(authors);
            }
        }
    }
}
