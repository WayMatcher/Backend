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
            try
            {
                var user = new UserDto
                {
                    UserId = mfaModel.UserId,
                    MfAtoken = mfaModel.Token
                };

                var result = _userService.AcceptMfA(user);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound(result);
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
