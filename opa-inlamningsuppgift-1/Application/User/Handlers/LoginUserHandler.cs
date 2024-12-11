using Application.Interfaces.RepositoryInterfaces;
using Application.User.Queries;
using Application.User.Queries.Helpers;
using MediatR;

namespace Application.User.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LoginUserHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.Users.FirstOrDefault(user => user.UserName == request.LoginUser.Username && user.Password == request.LoginUser.Password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);

            return await Task.FromResult(token);
        }
    }
}
