using Application.Author.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Author.Handlers
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, Domain.Models.Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Domain.Models.Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Models.Author author = await _authorRepository.GetAuthorById(request.id);

            if (author == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.id} was not found.");
            }

            try
            {
                await _authorRepository.GetAuthorById(request.id);
                return author;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the author ID. {ex}");
            }
        }
    }
}
