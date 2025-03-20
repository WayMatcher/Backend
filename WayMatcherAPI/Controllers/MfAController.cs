using Azure;
using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.Enums;
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

            if (result.Equals(RESTCode.Success))
                return Ok(result);
            else if (result.Equals(RESTCode.DbObjectNotFound))
                return NotFound(result);
            else if (result.Equals(RESTCode.ObjectNull))
                return NotFound(result);
            else
                return BadRequest(result);
        }
    }
}
