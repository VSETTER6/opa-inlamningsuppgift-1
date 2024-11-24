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
    // Klassdeklartion
    // Denna hanterar en förfårgan och retunerar en lista med böcker.
    // IRequestHandler kommer från MediatR och säger att klassen hantera en viss typ av förfrågan, i detta
    // fallet  GetAllBooksQuery.
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<BookModel>>
    {
        // Fältdeklarion
        // Representerar en databas där böckerna finns.
        private readonly IFakeDatabase _fakeDatabase;

        // Konstruktor
        // Tar emot databasen som parameter när ett objekt av klassen skapas.
        // Spara databasen i variablen _fakeDatabase, så att resten av klassen kan använda den.
        public GetAllBooksHandler(IFakeDatabase fakeDatabase)
        {
            _fakeDatabase = fakeDatabase;
        }

        // Handle
        // Här sker själva jobbet. Den tar emot GetAllBooksQuery request, alltså den frågar efter alla böcker.
        // Tar även en CancellationToken ifall man vill avbryta.
        // Retunerar en lista med böcker i en Task, vilket innebär att reslutat är asynkront (att det tar lite
        // tid att få resultatet).
        public Task<List<BookModel>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_fakeDatabase.GetAllBooks());
        }
    }
}
