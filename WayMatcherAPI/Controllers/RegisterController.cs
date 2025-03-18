using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser([FromBody] UserEditModel user)
        {
            var result = _userService.RegisterUser(user.User, user.Vehicle);

            if (result)
            {
                _userService.ConfigurateAddress(user.User);
                _userService.ConfigurateVehicle(user.User, user.Vehicle);

                return Ok(result);
            }
            else
                return StatusCode(500, "An error occurred while registering the user.");
        }
    }
}
