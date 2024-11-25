using Application.Book.Queries;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookModel>>
    {
        private readonly IFakeDatabase _fakeDatabase;

        public GetAllBooksHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        public Task<List<BookModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeDatabase.GetAllBooks());
        }
    }
}
