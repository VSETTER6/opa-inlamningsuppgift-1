using Application.User.DTOS;
using MediatR;

namespace Application.User.Queries
{
    public class LoginUserQuery : IRequest<string>
    {
        public LoginUserQuery(UserDto loginUser)
        {
            LoginUser = loginUser;
        }
        
        public UserDto LoginUser { get; }
    }
}
