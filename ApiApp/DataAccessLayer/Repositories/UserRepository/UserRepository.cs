using ApiApp.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.DataAccessLayer.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly APIDbContext _context;

        public UserRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users
                .FirstAsync(user => user.Id == id);
        }

        public async Task<User> GetUserAsync(string name)
        {
            return await _context.Users
                .FirstAsync(user => user.Name != null && user.Name.Equals(name));
        }

        public async Task<bool> PutUserAsync(User user)
        {
            var userUpdated = await _context.Users
                .FirstOrDefaultAsync(x=> x.Id == user.Id);

            if (userUpdated == null) 
            {
                return false;
            }

            userUpdated.Name = user.Name;
            userUpdated.Password = user.Password;
            userUpdated.PersonId = user.PersonId;
            userUpdated.Person = user.Person;

            _context.Users.Update(userUpdated);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task PostUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .FirstAsync(user => user.Id == id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
