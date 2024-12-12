using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        Task<User> GetUserById(Guid id);

        Task AddUser(User user);

        Task DeleteUser(Guid id);

        Task UpdateUser(Guid id, User user);

        Task<User> GetUserByCredentials(string username, string password);
    }
}
