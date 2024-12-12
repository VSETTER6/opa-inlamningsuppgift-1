using Application.Interfaces.RepositoryInterfaces;
using Application.User.Commands;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            Domain.Models.User user = await _userRepository.GetUserById(request.id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.id} was not found.");
            }

            try
            {
                await _userRepository.DeleteUser(request.id);
                return Unit.Value;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while deleting the user. {ex}");
            }
        }
    }
}
