using Application.Authors.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class AddAuthorHandler : IRequestHandler<AddAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                return OperationResult<Author>.Failed("None of first name, last name or category can be empty.");
            }
            else
            {
                Author newAuthor = new Author
                {
                    Id = Guid.NewGuid(),
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    Category = request.category
                };

                await _authorRepository.AddAuthor(newAuthor);
                return OperationResult<Author>.Successful(newAuthor);
            }
        }
    }
}
