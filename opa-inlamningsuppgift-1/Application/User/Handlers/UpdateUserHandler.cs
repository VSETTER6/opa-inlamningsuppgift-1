using Application.Interfaces.RepositoryInterfaces;
using Application.User.Commands;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Domain.Models.User>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task<Domain.Models.User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetUserById(request.id);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException($"User with ID {request.id} was not found.");
            }

            if (string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password))
            {
                throw new ArgumentException("None of username or password can be empty.");
            }

            try
            {
                userToUpdate.UserName = request.username;
                userToUpdate.Password = request.password;

                await _userRepository.UpdateUser(userToUpdate.Id, userToUpdate);

                return userToUpdate;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while updating the user. {ex}");
            }
        }
    }
}
