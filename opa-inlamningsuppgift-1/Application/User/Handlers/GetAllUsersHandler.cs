using Application.Interfaces.RepositoryInterfaces;
using Application.User.Queries;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<Domain.Models.User>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Domain.Models.User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetAllUsers();
                return users;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the users. {ex}");
            }
        }
    }
}
