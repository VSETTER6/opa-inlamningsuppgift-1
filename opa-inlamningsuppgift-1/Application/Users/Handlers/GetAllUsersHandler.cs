using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Queries;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, OperationResult<List<User>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();

            if (users == null)
            {
                return OperationResult<List<User>>.Failed("An error occurred while getting the users.");
            }
            else
            {
                return OperationResult<List<User>>.Successful(users);
            }
        }
    }
}
