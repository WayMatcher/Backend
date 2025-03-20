using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
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
        public IActionResult MfAInput([FromBody] RequestMFAModel mfaModel)
        {
            var user = new UserDto
            {
                Username = mfaModel.Username,
                Email = mfaModel.Email,
                MfAtoken = mfaModel.Token
            };

            var result = _userService.AcceptMfA(user);

            if (!string.IsNullOrEmpty(result))
                return Ok(result);
            else
                return StatusCode(500, "An error occurred while processing the MFA input.");
        }
    }
}
