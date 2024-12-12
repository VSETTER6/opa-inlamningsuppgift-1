using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<List<Domain.Models.Author>> GetAllAuthors();

        Task<Domain.Models.Author> GetAuthorById(Guid id);

        Task AddAuthor(Domain.Models.Author author);

        Task DeleteAuthor(Guid id);

        Task UpdateAuthor(Guid id, Domain.Models.Author author);
    }
}
