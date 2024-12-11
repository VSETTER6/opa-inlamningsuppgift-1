using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly RealDatabase _realDatabase;

        public BookRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        public async Task AddBook(BookModel book)
        {
            await _realDatabase.Books.AddAsync(book);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _realDatabase.Books.FindAsync(id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            _realDatabase.Books.Remove(book);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await _realDatabase.Books.ToListAsync();
        }

        public async Task<BookModel> GetBookById(Guid id)
        {
            var book = await _realDatabase.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            return book;
        }

        public async Task UpdateBook(Guid id, BookModel book)
        {
            var existingBook = await _realDatabase.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} was not found.");
            }

            existingBook.Title = book.Title;
            existingBook.Description = book.Description;

            await _realDatabase.SaveChangesAsync();
        }
    }
}
