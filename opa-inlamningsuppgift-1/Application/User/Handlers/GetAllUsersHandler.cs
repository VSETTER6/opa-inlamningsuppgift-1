//using Application.User.Queries;
//using Domain.Models;
//using MediatR;

//namespace Application.User.Handlers
//{
//    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
//    {
//        private readonly IFakeDatabase _fakeDatabase;

//        public GetAllUsersHandler(IFakeDatabase fakeDatabase)
//        {
//            _fakeDatabase = fakeDatabase;
//        }

//        public Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
//        {
//            List<UserModel> allUsersFromFakeDatabase = _fakeDatabase.Users;
//            return Task.FromResult(allUsersFromFakeDatabase);
//        }
//    }
//}
