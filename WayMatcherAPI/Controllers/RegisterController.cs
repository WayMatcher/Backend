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

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("NewUser")]
        public IActionResult NewUser([FromBody] RequestRegisterUser user)
        {
            try
            {
                var vehicleList = new List<VehicleDto>();
                var vehicleMappingList = new List<VehicleMappingDto>();

                foreach (var vehicle in user.VehicleList)
                {
                    var vehicleDto = new VehicleDto()
                    {
                        VehicleId = vehicle.VehicleId ?? -1,
                        Model = vehicle.Model,
                        Seats = vehicle.Seats,
                        YearOfManufacture = vehicle.YearOfManufacture,
                        ManufacturerName = vehicle.ManufacturerName
                    };

                    var vehicleMappingDto = new VehicleMappingDto()
                    {
                        FuelMilage = vehicle.FuelMilage,
                        AdditionalInfo = vehicle.AdditionalInfo,
                        LicensePlate = vehicle.LicensePlate,
                        VehicleId = vehicle.VehicleId ?? -1,
                    };

                    vehicleList.Add(vehicleDto);
                    vehicleMappingList.Add(vehicleMappingDto);
                }

                user.User.Password = user.Password;

                var result = _userService.RegisterUser(user.User, vehicleList, vehicleMappingList);

                if (result)
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
