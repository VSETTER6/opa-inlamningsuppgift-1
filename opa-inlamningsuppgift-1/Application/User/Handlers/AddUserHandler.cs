using Application.Interfaces.RepositoryInterfaces;
using Application.User.Commands;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    internal sealed class AddUserHandler : IRequestHandler<AddUserCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public AddUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<UserModel> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            UserModel userToAdd = new()
            {
                Id = Guid.NewGuid(),
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password
            };

            _userRepository.Users.Add(userToAdd);

            return Task.FromResult(userToAdd);
        }
    }
}
