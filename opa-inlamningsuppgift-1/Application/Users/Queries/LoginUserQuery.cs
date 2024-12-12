using Application.Users.DTOS;
using Domain.Models;
using MediatR;

namespace Application.Users.Queries
{
    public class LoginUserQuery : IRequest<OperationResult<string>>
    {
        public LoginUserQuery(UserDto loginUser)
        {
            LoginUser = loginUser;
        }
        
        public UserDto LoginUser { get; }
    }
}
