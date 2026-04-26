using Microsoft.AspNetCore.Mvc;
using OverlapssystemDomain.Entities;
using OverlapssytemApplication.Interfaces;
using OverlapssystemShared;

namespace OverlapssystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Hent alle
        [HttpGet("HenterBrugere")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Handle(result);
        }

        //Hent på ID
        [HttpGet("HenterBrugere/{userID}")]
        public async Task<IActionResult> GetUsersById(int userID)
        {
            var restult = await _userService.GetUserByIdAsync(userID);
            return Handle(restult);
        }

        //Tilføj
        [HttpPost("OpretBruger")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserDTO userDTO)
        {
            var usermodel = new UserModel
            {
                UserName = userDTO.UserName,
                UserPassword = userDTO.Password,
            };

            var newuser = await _userService.CreateNewUserAsync(usermodel);

            if(!newuser.Success) return Handle(newuser);

            return Created($"/api/Medicin/{newuser.Value}", newuser.Value);

        }

        //Slet
        [HttpDelete("{userID")]
        public async Task<IActionResult> DeleteUser(int userID)
        {
            await _userService.DeleteUserAsync(userID);
            return Ok(userID);
        }

        //Update
        [HttpPut("{userID")]
        public async Task<IActionResult> UpdateUser(int userID, [FromBody] UserModel usermodel)
        {
            await _userService.UpdateUserAsync(usermodel);
            return Ok(userID);
        }
    }
}
