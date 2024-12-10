using Application.Author.Queries;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Author.Handlers
{
    public class GetAuthorByIdHandler : IRequestHandler<GetAuthorByIdQuery, AuthorModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorByIdHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorModel> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.id == Guid.Empty)
            {
                throw new Exception("ID cannot be empty and must be GUID.");
            }

            try
            {
                var author = await _authorRepository.GetAuthorById(request.id);
                return author;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the author ID. {ex}");
            }
        }
    }
}
