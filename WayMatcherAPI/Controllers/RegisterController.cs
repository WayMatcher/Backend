using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Register")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="user">The user change request model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("NewUser")]
        public IActionResult NewUser([FromBody] RequestUserChange user)
        {
            return HandleRequest(() =>
            {
                var vehicleCounter = 0;
                foreach (var vehicle in user.VehicleList)
                {
                    vehicle.VehicleId = vehicleCounter++;
                }

                var vehicleList = MapToVehicleDtoList(user.VehicleList);
                var vehicleMappingList = MapToVehicleMappingDtoList(user.VehicleList);

                user.User.Password = user.Password;

                var result = _userService.RegisterUser(user.User, vehicleList, vehicleMappingList);

                return result ? Ok(result) : BadRequest(result);
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

        /// <summary>
        /// Maps the list of request vehicle models to a list of vehicle DTOs.
        /// </summary>
        /// <param name="vehicleList">The list of request vehicle models.</param>
        /// <returns>The list of vehicle DTOs.</returns>
        private List<VehicleDto> MapToVehicleDtoList(List<RequestVehicleModel> vehicleList)
        {
            return vehicleList.Select(vehicle => new VehicleDto
            {
                VehicleId = vehicle.VehicleId ?? -1,
                Model = vehicle.Model,
                Seats = vehicle.Seats,
                YearOfManufacture = vehicle.YearOfManufacture,
                ManufacturerName = vehicle.ManufacturerName
            }).ToList();
        }

        /// <summary>
        /// Maps the list of request vehicle models to a list of vehicle mapping DTOs.
        /// </summary>
        /// <param name="vehicleList">The list of request vehicle models.</param>
        /// <returns>The list of vehicle mapping DTOs.</returns>
        private List<VehicleMappingDto> MapToVehicleMappingDtoList(List<RequestVehicleModel> vehicleList)
        {
            return vehicleList.Select(vehicle => new VehicleMappingDto
            {
                VehicleId = vehicle.VehicleId ?? -1,
                FuelMilage = vehicle.FuelMilage,
                AdditionalInfo = vehicle.AdditionalInfo,
                LicensePlate = vehicle.LicensePlate,
            }).ToList();
        }
    }
}
