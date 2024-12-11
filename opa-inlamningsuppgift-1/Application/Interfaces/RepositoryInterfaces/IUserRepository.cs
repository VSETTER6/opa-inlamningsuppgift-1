using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();

        Task<UserModel> GetUserById(Guid id);

        Task AddUser(UserModel user);

        Task DeleteUser(Guid id);

        Task UpdateUser(Guid id, UserModel user);
    }
}
