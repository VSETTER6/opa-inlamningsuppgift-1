using Application.Authors.Commands;
using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using MediatR;

namespace Application.Authors.Handlers
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, OperationResult<Author>>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<OperationResult<Author>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            Author authorToUpdate = await _authorRepository.GetAuthorById(request.id);

            if (authorToUpdate == null)
            {
                return OperationResult<Author>.Failed($"Author with ID {request.id} was not found.");
            }
            if (string.IsNullOrWhiteSpace(request.firstName) || string.IsNullOrWhiteSpace(request.lastName) || string.IsNullOrWhiteSpace(request.category))
            {
                return OperationResult<Author>.Failed("First name, last name, and category cannot be empty.");
            }
            else
            {
                authorToUpdate.FirstName = request.firstName;
                authorToUpdate.LastName = request.lastName;
                authorToUpdate.Category = request.category;

                await _authorRepository.UpdateAuthor(authorToUpdate.Id, authorToUpdate);

                return OperationResult<Author>.Successful(authorToUpdate);
            }
        }
    }
}
