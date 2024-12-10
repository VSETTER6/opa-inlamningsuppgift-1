using Application.Author.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Author.Handlers
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, AuthorModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorModel> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            AuthorModel authorToUpdate = await _authorRepository.GetAuthorById(request.id);

            if (authorToUpdate == null)
            {
                throw new KeyNotFoundException($"Author with ID {request.id} not found.");
            }

            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                throw new ArgumentException("First name, last name, and category cannot be empty.");
            }

            try
            {
                authorToUpdate.FirstName = request.firstName;
                authorToUpdate.LastName = request.lastName;
                authorToUpdate.Category = request.category;

                await _authorRepository.UpdateAuthor(authorToUpdate.Id, authorToUpdate);

                return authorToUpdate;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the author. {ex}");
            }
        }
    }
}
