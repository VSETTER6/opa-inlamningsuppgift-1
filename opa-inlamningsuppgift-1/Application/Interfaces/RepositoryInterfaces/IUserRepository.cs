using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(Guid id);

        Task AddUser(UserModel user);

        Task DeleteUser(Guid id);

        Task UpdateUser(Guid id, UserModel user);

        Task<UserModel> GetUserByCredentials(string username, string password);
    }
}
