using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
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
            try
            {
                var user = new UserDto
                {
                    Username = userLogin.Username,
                    Email = userLogin.Email,
                    Password = userLogin.Password
                };

                var result = _userService.LoginUser(user)?.UserId;

                if (result != -1)
                    return Ok(result);
                else
                    return NotFound("User not found or invalid credentials.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] UserDto user)
        {
            try
            {
                _userService.SendChangePasswordMail(user);
                return Ok("Password reset email sent.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] UserDto user)
        {
            try
            {
                var result = _userService.ChangePassword(user);
                if (result)
                    return Ok(result);
                else
                    return StatusCode(500, "An error occurred while changing Password user.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
