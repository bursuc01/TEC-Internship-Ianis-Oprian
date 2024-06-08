using ApiApp.DataAccessLayer.Model;

namespace ApiApp.DataAccessLayer.Repositories.UserRepository
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetUsersAsync();
        public Task<User> GetUserAsync(int id);
        public Task<User> GetUserAsync(string name);
        public Task<bool> PutUserAsync(User user);
        public Task PostUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
