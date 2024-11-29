using Application.User.Queries;
using Application.User.Queries.Helpers;
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
        private readonly TokenHelper _tokenHelper;

        public LoginUserHandler(IFakeDatabase fakeDatabase, TokenHelper tokenHelper)
        {
            _fakeDatabase = fakeDatabase;
            _tokenHelper = tokenHelper;
        }

        public Task<string> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = _fakeDatabase.Users.FirstOrDefault(user => user.UserName == request.LoginUser.UserName && user.Password == request.LoginUser.Password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            string token = _tokenHelper.GenerateJwtToken(user);

            return Task.FromResult(token);
        }
    }
}
