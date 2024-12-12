using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();

        Task<Book> GetBookById(Guid id);

        Task AddBook(Book book);

        Task DeleteBook(Guid id);

        Task UpdateBook(Guid id, Book book);
    }
}
