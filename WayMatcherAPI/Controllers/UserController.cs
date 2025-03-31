using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WayMatcherAPI.Models;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Edits the user information.
        /// </summary>
        /// <param name="userEdit">The user edit request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("EditUser")]
        public IActionResult EditUser([FromBody] RequestUserChange userEdit)
        {
            return HandleRequest(() =>
            {
                var isVehicleConfigurated = false;

                if (userEdit.VehicleList != null)
                {
                    var vehicleList = userEdit.VehicleList.Select(vehicle => new VehicleDto
                    {
                        VehicleId = vehicle.VehicleId ?? -1,
                        Model = vehicle.Model,
                        Seats = vehicle.Seats,
                        YearOfManufacture = vehicle.YearOfManufacture,
                        ManufacturerName = vehicle.ManufacturerName
                    }).ToList();

                    var vehicleMappingList = userEdit.VehicleList.Select(vehicle => new VehicleMappingDto
                    {
                        VehicleId = vehicle.VehicleId ?? -1,
                        FuelMilage = vehicle.FuelMilage,
                        AdditionalInfo = vehicle.AdditionalInfo,
                        LicensePlate = vehicle.LicensePlate,
                    }).ToList();

                    isVehicleConfigurated = _userService.ConfigurateVehicle(userEdit.User, vehicleList, vehicleMappingList);
                }
                else
                    isVehicleConfigurated = true;

                if(!userEdit.Password.IsNullOrEmpty())
                    userEdit.User.Password = userEdit.Password;

                var result =  isVehicleConfigurated && _userService.ConfigurateUser(userEdit.User);

                return result ? Ok(result) : StatusCode(500, "An error occurred while editing the user.");
            });
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("DeleteUser")]
        public IActionResult DeleteUser([FromBody] RequestUser user)
        {
            return HandleRequest(() =>
            {
                var userDto = new UserDto
                {
                    UserId = user.UserId ?? -1,
                    Username = user.Username,
                    Email = user.Email
                };
                var result = _userService.DeleteUser(userDto);
                return result ? Ok(result) : StatusCode(500, "An error occurred while deleting the user.");
            });
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="user">The user request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetUser")]
        public IActionResult GetUser([FromQuery] RequestUser user)
        {
            return HandleRequest(() =>
            {
                var userDto = MapToUserDto(user);
                var result = _userService.GetUser(userDto);
                return result != null ? Ok(result) : NotFound("User not found.");
            });
        }

        /// <summary>
        /// Gets the address of the user.
        /// </summary>
        /// <param name="user">The user request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetAddress")]
        public IActionResult GetAddress([FromQuery] RequestUser user)
        {
            return HandleRequest(() =>
            {
                var userDto = MapToUserDto(user);
                var result = _userService.GetAddress(userDto);
                return result != null ? Ok(result) : NotFound("Address not found.");
            });
        }

        /// <summary>
        /// Gets the list of vehicles associated with the user.
        /// </summary>
        /// <param name="user">The user request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetVehicleList")]
        public IActionResult GetVehicleList([FromQuery] RequestUser user)
        {
            Console.WriteLine("Request Headers:");
            foreach (var header in Request.Headers)
            {
                Console.WriteLine($"{header.Key}: {header.Value}");
            }
            return HandleRequest(() =>
            {
                var userDto = MapToUserDto(user);
                var result = _userService.GetUserVehicleList(userDto);
                return result != null ? Ok(result) : NotFound("Vehicle not found.");
            });
        }

        /// <summary>
        /// Rates the user.
        /// </summary>
        /// <param name="rateUser">The rate user request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("RateUser")]
        public IActionResult RateUser([FromBody] RequestRateUser rateUser)
        {
            return HandleRequest(() =>
            {
                var rate = new RatingDto
                {
                    RatingText = rateUser.RatingText,
                    RatingValue = rateUser.RatingValue,
                    RatedUserId = rateUser.RatedUserId,
                    UserWhoRatedId = rateUser.UserWhoRatedId,
                    StatusId = (int)State.Active
                };

                var result = _userService.RateUser(rate);
                return result ? Ok(result) : StatusCode(500, "An error occurred while rating the user.");
            });
        }

        /// <summary>
        /// Gets the user rating.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetUserRating")]
        public IActionResult GetUserRating([FromQuery] int userId)
        {
            return HandleRequest(() =>
            {
                var rating = new RatingDto
                {
                    RatedUserId = userId
                };

                var result = _userService.UserRating(rating);
                return result != -1 ? Ok(result) : NotFound("No rated users found.");
            });
        }

        /// <summary>
        /// Sends a notification to the user.
        /// </summary>
        /// <param name="notificationDto">The notification DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("SendNotification")]
        public IActionResult SendNotification([FromBody] NotificationDto notificationDto)
        {
            return HandleRequest(() =>
            {
                var result = _userService.SendNotification(notificationDto);
                return result != null ? Ok(result) : NotFound("No notifications found.");
            });
        }

        /// <summary>
        /// Marks a notification as read.
        /// </summary>
        /// <param name="notificationId">The notification identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("ReadNotification")]
        public IActionResult ReadNotification([FromBody] int notificationId)
        {
            return HandleRequest(() =>
            {
                var notification = new NotificationDto
                {
                    NotificationId = notificationId,
                    Read = true
                };
                var result = _userService.UpdateNotification(notification);
                return result ? Ok(result) : NotFound("No notifications found.");
            });
        }

        /// <summary>
        /// Gets the list of notifications for the user.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetNotification")]
        public IActionResult GetNotification([FromQuery] int userID)
        {
            return HandleRequest(() =>
            {
                var user = new UserDto
                {
                    UserId = userID
                };

                var result = _userService.GetNotification(user);
                return result != null ? Ok(result) : NotFound("No notifications found.");
            });
        }

        /// <summary>
        /// Gets the status of the API.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetStatus")]
        public IActionResult GetStatus()
        {
            return Ok();
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

        /// <summary>
        /// Maps the request user model to a user DTO.
        /// </summary>
        /// <param name="user">The request user model.</param>
        /// <returns>The user DTO.</returns>
        private UserDto MapToUserDto(RequestUser user)
        {
            return new UserDto
            {
                UserId = user.UserId ?? -1,
                Username = user.Username,
                Email = user.Email,
            };
        }
    }
}
