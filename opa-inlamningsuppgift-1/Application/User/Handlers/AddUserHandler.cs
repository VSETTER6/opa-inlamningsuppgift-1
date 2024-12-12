using Application.Interfaces.RepositoryInterfaces;
using Application.User.Commands;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    internal sealed class AddUserHandler : IRequestHandler<AddUserCommand, Domain.Models.User>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Domain.Models.User> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.userDto.Username) || string.IsNullOrWhiteSpace(request.userDto.Password))
            {
                throw new ArgumentException("None of username or password can be empty.");
            }

            try
            {
                Domain.Models.User newUser = new Domain.Models.User
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
