using ApiApp.BusinessLogicLayer.UserBLL;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInformation>>> GetUsersAsync()
        {
            var users = await _userService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserInformation>> GetUserAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUserAsync(UserInformation user)
        {
            await _userService.PutUserAsync(user);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<UserInformation>> PostUserAsync(UserInformation user)
        {
            await _userService.PostUserAsync(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }
    }
}
