using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Queries;
using Application.Users.Queries.Helpers;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, OperationResult<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenHelper _tokenHelper;

        public LoginUserHandler(IUserRepository userRepository, TokenHelper tokenHelper)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<OperationResult<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserByCredentials(request.LoginUser.Username, request.LoginUser.Password);

            if (user == null)
            {
                return OperationResult<string>.Failed("Invalid username or password");
            }
            else
            {
                string token = _tokenHelper.GenerateJwtToken(user.Result);

                return OperationResult<string>.Successful(token);
            }
        }
    }
}
