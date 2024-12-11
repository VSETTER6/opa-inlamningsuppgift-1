using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllBooks();

        Task<BookModel> GetBookById(Guid id);

        Task AddBook(BookModel book);

        Task DeleteBook(Guid id);

        Task UpdateBook(Guid id, BookModel book);
    }
}
