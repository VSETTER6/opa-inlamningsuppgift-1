using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _realDatabase;

        public BookRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task<BookModel> AddBook(BookModel book)
        {
            _realDatabase.Books.Add(book);
            _realDatabase.SaveChanges();
            return book;
        }

        public Task<string> DeleteBook(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookModel>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<BookModel> GetBookById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BookModel> UpdateBook(Guid id, BookModel book)
        {
            throw new NotImplementedException();
        }
    }
}
