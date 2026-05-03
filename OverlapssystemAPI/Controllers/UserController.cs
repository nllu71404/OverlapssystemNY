using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemShared;
using Microsoft.AspNetCore.Authorization;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        //Hent alle
        [HttpGet("HenterBrugere")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Handle(result);
        }

        //Hent på ID
        [HttpGet("HenterBrugere/{userId}")]
        public async Task<IActionResult> GetUsersById(string userId)
        {
            var restult = await _userService.GetUserByIdAsync(userId);
            return Handle(restult);
        }

        //Hent på brugernavn
        [HttpGet("HenterBrugere/Brugernavn/{username}")]
        public async Task<IActionResult> GetUsersByUsername(string username)
        {
            var restult = await _userService.GetUserByUserNameAsync(username);
            return Handle(restult);
        }

        //Tilføj
        [HttpPost("OpretBruger")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO userDTO)
        {
            var userModel = new UserModel
            {
                UserName = userDTO.UserName,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DepartmentId = userDTO.DepartmentId
            };
            var result = await _userService.CreateNewUserAsync(userModel, userDTO.Password, userDTO.Role);
            return Handle(result);
        }

        //Slet
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok(userId);
        }

        //Update
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UserModel usermodel)
        {
            await _userService.UpdateUserAsync(userId, usermodel);
            return Ok(userId);
        }
        //Validering
        [AllowAnonymous]
        [HttpPost("ValiderBruger")]
        public async Task<IActionResult> ValidateUser([FromBody] AddUserDTO userDTO)
        {
            var result = await _authService.LoginAsync(userDTO.UserName, userDTO.Password);
            if (!result.Success)
            {
                return Unauthorized(new { message = result.Error.Message ?? "Ingen adgang" });
            }

            return Ok(new { token = result.Value});
        }
    }
}
