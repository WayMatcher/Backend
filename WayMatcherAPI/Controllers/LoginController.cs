using Microsoft.AspNetCore.Mvc;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("LoginUser")]
        public IActionResult Login([FromBody] UserDto user)
        {
            var result = _userService.LoginUser(user);

            if (result)
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while logging in.");
        }
    }
}
