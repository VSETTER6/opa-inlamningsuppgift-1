using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Queries;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, OperationResult<User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public async Task<OperationResult<User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserById(request.id);

            if (user == null)
            {
                return OperationResult<User>.Failed($"User with ID {request.id} was not found.");
            }
            else
            {
                await _userRepository.GetUserById(request.id);
                return OperationResult<User>.Successful(user);
            }
        }
    }
}
