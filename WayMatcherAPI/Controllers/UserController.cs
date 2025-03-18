using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        private bool UpdateUserDetails(UserEditModel userEdit)
        {
            return _userService.ConfigurateAddress(userEdit.User) && _userService.ConfigurateVehicle(userEdit.User, userEdit.Vehicle) && _userService.ConfigurateUser(userEdit.User);
        }

        [HttpPost("EditUser")]
        public IActionResult EditUser([FromBody] UserEditModel userEdit)
        {
            var result = UpdateUserDetails(userEdit);

            if (result)
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while editing the user.");
        }

        [HttpPost("ChangePassword")]
        public IActionResult SendPasswordEmail([FromBody] UserDto user)
        {
            var result = _userService.ChangePassword(user);

            if (result)
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while editing the user.");
        }

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser([FromBody] UserDto user)
        {
            var result = _userService.DeleteUser(user);
            if (result)
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while deleting the user.");
        }
    }
}
