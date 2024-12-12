using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IBookRepository
    {
        Task<List<Domain.Models.Book>> GetAllBooks();

        Task<Domain.Models.Book> GetBookById(Guid id);

        Task AddBook(Domain.Models.Book book);

        Task DeleteBook(Guid id);

        Task UpdateBook(Guid id, Domain.Models.Book book);
    }
}
