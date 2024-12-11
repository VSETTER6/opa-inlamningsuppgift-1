using Application.Interfaces.RepositoryInterfaces;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RealDatabase _realDatabase;

        public UserRepository(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }

        private static List<UserModel> userModelList = new();

        public List<UserModel> Users 
        {
            get { return userModelList; }
            set { userModelList = value; }
        }

        public async Task AddUser(UserModel user)
        {
            await _realDatabase.Users.AddAsync(user);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _realDatabase.Users.FindAsync(id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }

            _realDatabase.Users.Remove(user);
            await _realDatabase.SaveChangesAsync();
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _realDatabase.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(Guid id)
        {
            var user = await _realDatabase.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }

            return user;
        }

        public async Task UpdateUser(Guid id, UserModel user)
        {
            var existingUser = await _realDatabase.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {id} was not found.");
            }

            existingUser.UserName = user.UserName;
            existingUser.Password = user.Password;

            await _realDatabase.SaveChangesAsync();
        }
    }
}
