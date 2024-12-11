using Domain.Models;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        List<UserModel> Users { get; set; }

        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(Guid id);

        Task AddUser(UserModel user);

        Task DeleteUser(Guid id);

        Task UpdateUser(Guid id, UserModel user);
    }
}
