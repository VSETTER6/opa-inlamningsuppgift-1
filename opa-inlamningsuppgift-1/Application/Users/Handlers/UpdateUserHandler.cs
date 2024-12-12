using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Commands;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task<OperationResult<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _userRepository.GetUserById(request.id);

            if (userToUpdate == null)
            {
                return OperationResult<User>.Failed($"User with ID {request.id} was not found.");
            }
            if (string.IsNullOrWhiteSpace(request.username) || string.IsNullOrWhiteSpace(request.password))
            {
                return OperationResult<User>.Failed("None of username or password can be empty.");
            }
            else
            {
                userToUpdate.UserName = request.username;
                userToUpdate.Password = request.password;

                await _userRepository.UpdateUser(userToUpdate.Id, userToUpdate);

                return OperationResult<User>.Successful(userToUpdate);
            }
        }
    }
}
