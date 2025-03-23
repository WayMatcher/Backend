using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        private bool UpdateUserDetails(RequestRegisterUser userEdit)
        {
            return _userService.ConfigurateAddress(userEdit.User) && _userService.ConfigurateVehicle(userEdit.User, userEdit.Vehicle) && _userService.ConfigurateUser(userEdit.User);
        }

        [HttpPost("EditUser")]
        public IActionResult EditUser([FromBody] RequestRegisterUser userEdit)
        {
            try
            {
                var result = UpdateUserDetails(userEdit);
                if (result)
                    return Ok(result);
                else
                    return StatusCode(500, "An error occurred while editing the user.");
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

        [HttpPost("ChangePassword")]
        public IActionResult SendPasswordEmail([FromBody] UserDto user)
        {
            try
            {
                var result = _userService.ChangePassword(user);
                if (result)
                    return Ok(result);
                else
                    return StatusCode(500, "An error occurred while sending the Password E-Mail.");
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

        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser([FromBody] UserDto user)
        {
            try
            {
                var result = _userService.DeleteUser(user);
                if (result)
                    return Ok(result);
                else
                    return StatusCode(500, "An error occurred while deleting the user.");
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

        [HttpGet("GetUser")]
        public IActionResult GetUser([FromQuery] RequestUser user)
        {
            try
            {
                var userDto = new UserDto()
                {
                    UserId = user.UserId ?? -1,
                    Username = user.Username,
                    Email = user.Email,
                };
                var result = _userService.GetUser(userDto);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound("User not found.");
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

        [HttpGet("GetAddress")]
        public IActionResult GetAddress([FromQuery] RequestUser user)
        {
            try
            {
                var userDto = new UserDto()
                {
                    UserId = user.UserId ?? -1,
                    Username = user.Username,
                    Email = user.Email,
                };
                var result = _userService.GetAddress(userDto);

                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Address not found.");
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

        [HttpGet("GetVehicleList")]
        public IActionResult GetVehicleList([FromQuery] RequestUser user)
        {
            Console.WriteLine("Request Headers:");
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }
            try
            {
                var userDto = new UserDto()
                {
                    UserId = user.UserId ?? -1,
                    Username = user.Username,
                    Email = user.Email,
                };
                var result = _userService.GetUserVehicleList(userDto);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Vehicle not found.");
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

        [HttpPost("RateUser")]
        public IActionResult RateUser([FromBody] RequestRateUser rateUser)
        {
            try
            {
                var rate = new RatingDto
                {
                    RatingText = rateUser.RatingText,
                    RatingValue = rateUser.RatingValue,
                    RatedUserId = rateUser.RatedUserId,
                    UserWhoRatedId = rateUser.UserWhoRatedId,
                    StatusId = rateUser.StatusId
                };

                var result = _userService.RateUser(rate);
                if (result)
                    return Ok(result);
                else
                    return StatusCode(500, "An error occurred while rating the user.");
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
        [HttpPost("GetRatedUser")]
        public IActionResult GetUserRating([FromBody] UserDto user)
        {
            try
            {
                var rating = new RatingDto
                {
                    RatedUserId = user.UserId
                };

                var result = _userService.UserRating(rating);
                if (result != -1)
                    return Ok(result);
                else
                    return NotFound("No rated users found.");
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

        [HttpPost("SendNotification")]
        public IActionResult SendNotification([FromBody] NotificationDto notificationDto)
        {
            try
            {
                var result = _userService.SendNotification(notificationDto);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No notifications found.");
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

        [HttpGet("GetNotification")]
        public IActionResult GetNotification([FromQuery] UserDto user)
        {
            try
            {
                var result = _userService.GetNotification(user);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No notifications found.");
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
