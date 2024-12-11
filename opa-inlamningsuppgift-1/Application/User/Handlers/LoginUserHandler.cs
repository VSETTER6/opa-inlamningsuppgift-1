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
            var user = _userRepository.GetUserByCredentials(request.LoginUser.Username, request.LoginUser.Password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user.Result);

            return await Task.FromResult(token);
        }
    }
}
