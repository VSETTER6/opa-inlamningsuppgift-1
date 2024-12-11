﻿using Application.Interfaces.RepositoryInterfaces;
using Application.User.Queries;
using Domain.Models;
using MediatR;

namespace Application.User.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            UserModel user = await _userRepository.GetUserById(request.id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.id} was not found.");
            }

            try
            {
                await _userRepository.GetUserById(request.id);
                return user;
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"An error occurred while getting the user ID. {ex}");
            }
        }
    }
}
