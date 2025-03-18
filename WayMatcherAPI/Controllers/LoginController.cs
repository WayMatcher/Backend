using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Enums;
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
        public IActionResult Login([FromBody] RequestUserLoginModel userLogin)
        {
            var user = new UserDto
            {
                Username = userLogin.Username,
                Email = userLogin.Email,
                Password = userLogin.Password
            };

            var result = _userService.LoginUser(user);

            if (result.Equals(RESTCode.Ok))
                return Ok(result);
            else if (result.Equals(RESTCode.DbObjectNotFound))
                return NotFound(result);
            else if (result.Equals(RESTCode.ObjectNull))
                return NotFound(result);
            else
                return BadRequest(result);
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] UserDto user)
        {
            _userService.SendChangePasswordMail(user);
            return Ok();
        }
    }
}
