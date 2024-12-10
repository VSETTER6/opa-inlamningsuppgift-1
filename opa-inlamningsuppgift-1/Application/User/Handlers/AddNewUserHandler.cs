//using Application.User.Commands;
//using Domain.Models;
//using MediatR;

//namespace Application.User.Handlers
//{
//    internal sealed class AddNewUserHandler : IRequestHandler<AddNewUserCommand, UserModel>
//    {
//        private readonly IFakeDatabase _fakeDatabase;

//        public AddNewUserHandler(IFakeDatabase fakeDatabase)
//        {
//            _fakeDatabase = fakeDatabase;
//        }

//        public Task<UserModel> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
//        {
//            UserModel userToAdd = new()
//            {
//                Id = Guid.NewGuid(),
//                UserName = request.NewUser.UserName,
//                Password = request.NewUser.Password
//            };

//            _fakeDatabase.Users.Add(userToAdd);

//            return Task.FromResult(userToAdd);
//        }
//    }
//}
