using Application.Author.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Author.Handlers
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, AuthorModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorModel> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                throw new ArgumentException("None of first name, last name or category can be empty.");
            }

            try
            {
                AuthorModel newAuthor = new AuthorModel
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Category = request.category
                };

                await _authorRepository.AddAuthor(newAuthor);
                return newAuthor;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while adding the author. {ex}");
            }
        }
    }
}
