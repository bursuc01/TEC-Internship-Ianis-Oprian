using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Repositories.UserRepository;
using AutoMapper;

namespace ApiApp.BusinessLogicLayer.UserBLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserInformation>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<UserInformation>>(users);
        }

        public async Task<UserInformation> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserAsync(id);
            return _mapper.Map<UserInformation>(user);
        }

        public async Task<bool> PutUserAsync(UserInformation user)
        {
            var actualUser = _mapper.Map<User>(user);
            return await _userRepository.PutUserAsync(actualUser);
        }

        public async Task PostUserAsync(UserInformation user)
        {
            var actualUser = _mapper.Map<User>(user);
            await _userRepository.PostUserAsync(actualUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
        
    }
}
