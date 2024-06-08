using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.BusinessLogicLayer.UserBLL
{
    public interface IUserService
    {
        public Task<IEnumerable<UserInformation>> GetUsersAsync();
        public Task<UserInformation> GetUserByIdAsync(int id);
        public Task<bool> PutUserAsync(UserInformation user);
        public Task PostUserAsync(UserInformation user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
