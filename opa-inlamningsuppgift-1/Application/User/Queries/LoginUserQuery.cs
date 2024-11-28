using Application.User.DTOS;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
