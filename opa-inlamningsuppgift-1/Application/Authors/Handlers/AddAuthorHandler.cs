using Application.Authors.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, Domain.Models.Author>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Domain.Models.Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                throw new ArgumentException("None of first name, last name or category can be empty.");
            }

            try
            {
                Domain.Models.Author newAuthor = new Domain.Models.Author
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
