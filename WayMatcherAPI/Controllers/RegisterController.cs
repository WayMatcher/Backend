using Microsoft.AspNetCore.Mvc;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [ApiController]
    [Route("api/Register")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser([FromBody] UserDto newUser)
        {
            var result = _userService.RegisterUser(newUser);

            if (result)
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while registering the user.");
        }
    }
}
