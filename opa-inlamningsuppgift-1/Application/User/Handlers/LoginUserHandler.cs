using Application.User.Queries;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserQuery, string>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public LoginUserHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _fakeDatabase.Users.FirstOrDefault(user => user.UserName == request.LoginUser.UserName && user.Password == request.LoginUser.Password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            string token = "TOKEN TO RETURN";

            return Task.FromResult(token);
        }
    }
}
