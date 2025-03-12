using Microsoft.AspNetCore.Mvc;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/MfA")]
    [ApiController]
    public class MfAController : ControllerBase
    {
        private readonly IUserService _userService;

        public MfAController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("MfAInput")]
        public IActionResult MfAInput([FromBody] UserDto user)
        {
            var result = _userService.AcceptMfA(user);

            if (!string.IsNullOrEmpty(result))
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while processing the MFA input.");
        }
    }
}
