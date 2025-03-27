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

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Logs in the user.
        /// </summary>
        /// <param name="userLogin">The user login request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("LoginUser")]
        public IActionResult Login([FromBody] RequestUserLoginModel userLogin)
        {
            return HandleRequest(() =>
            {
                var user = new UserDto
                {
                    Username = userLogin.Username,
                    Email = userLogin.Email,
                    Password = userLogin.Password
                };

                var result = _userService.LoginUser(user)?.UserId;

                return result != -1 ? Ok(result) : NotFound("User not found or invalid credentials.");
            });
        }

        /// <summary>
        /// Sends a password reset email to the user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword([FromBody] UserDto user)
        {
            return HandleRequest(() =>
            {
                _userService.SendChangePasswordMail(user);
                return Ok("Password reset email sent.");
            });
        }

        /// <summary>
        /// Changes the password of the user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] UserDto user)
        {
            return HandleRequest(() =>
            {
                var result = _userService.ChangePassword(user);
                return result ? Ok(result) : StatusCode(500, "An error occurred while changing the password.");
            });
        }

        /// <summary>
        /// Handles the request and returns the appropriate response.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        private IActionResult HandleRequest(Func<IActionResult> action)
        {
            try
            {
                return action();
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
