using Application.User.DTOS;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands
{
    public class AddNewUserCommand : IRequest<UserModel>
    {
        public AddNewUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }

        public UserDto NewUser { get; }
    }
}
