using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAuthors();

        Task<Author> GetAuthorById(Guid id);

        Task AddAuthor(Author author);

        Task DeleteAuthor(Guid id);

        Task UpdateAuthor(Guid id, Author author);
    }
}
