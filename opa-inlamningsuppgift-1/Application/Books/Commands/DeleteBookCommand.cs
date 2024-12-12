﻿using MediatR;

namespace Application.Books.Commands
{
    public record DeleteBookCommand(Guid id) : IRequest<Unit>;
}