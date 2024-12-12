using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Commands;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    internal sealed class AddUserHandler : IRequestHandler<AddUserCommand, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<User>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.userDto.Username) || string.IsNullOrWhiteSpace(request.userDto.Password))
            {
                return OperationResult<User>.Failed("None of username or password can be empty.");
            }
            else
            {
                User newUser = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = request.userDto.Username,
                    Password = request.userDto.Password,
                };

                await _userRepository.AddUser(newUser);
                return OperationResult<User>.Successful(newUser);
            }
        }
    }
}
