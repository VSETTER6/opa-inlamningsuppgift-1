using Application.Interfaces.RepositoryInterfaces;
using Application.Users.Commands;
using Domain.Models;
using MediatR;

namespace Application.Users.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, OperationResult<Unit>>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<Unit>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserById(request.id);

            if (user == null)
            {
                return OperationResult<Unit>.Failed($"User with ID {request.id} was not found.");
            }
            else
            {
                await _userRepository.DeleteUser(request.id);
                return OperationResult<Unit>.Successful(Unit.Value);
            }
        }
    }
}
