using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<List<Domain.Models.User>> GetAllUsers();

        Task<Domain.Models.User> GetUserById(Guid id);

        Task AddUser(Domain.Models.User user);

        Task DeleteUser(Guid id);

        Task UpdateUser(Guid id, Domain.Models.User user);

        Task<Domain.Models.User> GetUserByCredentials(string username, string password);
    }
}
