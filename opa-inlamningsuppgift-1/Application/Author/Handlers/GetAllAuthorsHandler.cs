using Application.Author.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Author.Handlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, List<Domain.Models.Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Domain.Models.Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _authorRepository.GetAllAuthors();
                return authors;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the authors. {ex}");
            }
        }
    }
}
