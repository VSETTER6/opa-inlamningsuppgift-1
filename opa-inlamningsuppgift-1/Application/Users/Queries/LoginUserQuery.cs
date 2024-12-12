using Application.Users.DTOS;
using MediatR;

namespace Application.Users.Queries
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
