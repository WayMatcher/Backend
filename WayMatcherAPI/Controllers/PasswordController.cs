using Microsoft.AspNetCore.Mvc;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Password")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IUserService _userService;

        public PasswordController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("EditUser")]
        public IActionResult EditUser([FromBody] UserDto user)
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
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
