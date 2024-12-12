using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Commands;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    internal sealed class AddUserHandler : IRequestHandler<AddUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.userDto.Username) || string.IsNullOrWhiteSpace(request.userDto.Password))
            {
                throw new ArgumentException("None of username or password can be empty.");
            }

            try
            {
                User newUser = new Domain.Models.User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.userDto.Username,
                    Password = request.userDto.Password,
                };

                await _userRepository.AddUser(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while adding the user. {ex}");
            }
        }
    }
}
