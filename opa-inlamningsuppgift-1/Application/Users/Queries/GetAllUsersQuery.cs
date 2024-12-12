﻿using Domain.Models;
using MediatR;

namespace Application.Users.Queries
{
    public record GetAllUsersQuery : IRequest<List<User>>;
}