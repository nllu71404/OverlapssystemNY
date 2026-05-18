using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssystemShared;
using OverlapssytemApplication.Common;
using OverlapssytemApplication.Interfaces;

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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();

            if (!result.Success)
            {
                return Handle(result);
            }

            var userDTOs = result.Value.Select(MapToUserDTO).ToList();

            return Handle(Result.Ok(userDTOs));
        }

        //Hent på ID
        [HttpGet("HenterBrugere/{userId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsersById(string userId)
        {
            var result = await _userService.GetUserByIdAsync(userId);

            if (!result.Success)
            {
                return Handle(result);
            }

            var userDTO = MapToUserDTO(result.Value);

            return Handle(Result.Ok(userDTO));
        }

        //Hent på brugernavn
        [HttpGet("HenterBrugere/Brugernavn/{username}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsersByUsername(string username)
        {
            var result = await _userService.GetUserByUserNameAsync(username);

            if (!result.Success)
            {
                return Handle(result);
            }

            var userDTO = MapToUserDTO(result.Value);

            return Handle(Result.Ok(userDTO));
        }

        //Tilføj
        [HttpPost("OpretBruger")]
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _userService.DeleteUserAsync(userId);
            return Handle(result);
        }

        //Update
        [HttpPut("{userId}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDTO userDTO)
        {
            var userModel = new UserModel
            {
                UserName = userDTO.UserName,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                DepartmentId = userDTO.DepartmentId
            };

            var result = await _userService.UpdateUserAsync(userId, userModel);
            return Handle(result);
        }

        [AllowAnonymous]
        [HttpPost("ValiderBruger")]
        public async Task<IActionResult> ValidateUser([FromBody] AddUserDTO userDTO)
        {
            var result = await _authService.LoginAsync(userDTO.UserName, userDTO.Password);

            if (!result.Success)
            {
                return Unauthorized(new { message = result.Error.Message ?? "Ingen adgang" });
            }

            return Ok(new { token = result.Value });
        }

        //// ----- Mapping ---- //
        private static UserDTO MapToUserDTO(UserModel user)
        {
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName ?? "",
                FirstName = user.FirstName,
                LastName = user.LastName,
                DepartmentId = user.DepartmentId
            };
        }
    }
}
