using Application.User.Queries;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public GetAllUsersHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserModel> allUsersFromFakeDatabase = _fakeDatabase.Users;
            return Task.FromResult(allUsersFromFakeDatabase);
        }
    }
}
