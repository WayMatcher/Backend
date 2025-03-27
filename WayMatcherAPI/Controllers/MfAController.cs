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

        /// <summary>
        /// Initializes a new instance of the <see cref="MfAController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public MfAController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Accepts the MFA input from the user.
        /// </summary>
        /// <param name="mfaModel">The MFA request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("MfAInput")]
        public IActionResult MfAInput([FromBody] RequestMFAModel mfaModel)
        {
            return HandleRequest(() =>
            {
                var user = new UserDto
                {
                    UserId = mfaModel.UserId,
                    MfAtoken = mfaModel.Token
                };

                var result = _userService.AcceptMfA(user);

                return result != null ? Ok(result) : NotFound(result);
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
