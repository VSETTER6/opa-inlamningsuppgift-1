using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IAuthorRepository
    {
        Task<List<AuthorModel>> GetAllAuthors();

        Task<AuthorModel> GetAuthorById(Guid id);

        Task AddAuthor(AuthorModel author);

        Task DeleteAuthor(Guid id);

        Task UpdateAuthor(Guid id, AuthorModel author);
    }
}
