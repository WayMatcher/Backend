using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;

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
        public IActionResult NewUser([FromBody] RequestRegisterUser user)
        {
            try
            {
                var result = _userService.RegisterUser(user.User, user.VehicleList);

                if (result.Equals(RESTCode.Success))
                    return Ok(result);
                else
                    return BadRequest(result);
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
