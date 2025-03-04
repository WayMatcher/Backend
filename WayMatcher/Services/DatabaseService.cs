using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;
using WayMatcherBL.Mapper;
using WayMatcherBL.Models;

namespace WayMatcherBL.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly WayMatcherContext _dbContext;
        private ModelMapper _mapper;

        public DatabaseService(WayMatcherContext dbContext, ModelMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AddressDto> GetActiveAddresses()
        {
            var addressList = new List<AddressDto>();
            foreach (var address in _dbContext.Addresses.Where(a => a.Status.StatusDescription.Equals(State.Active)).ToList())
            {
                addressList.Add(_mapper.ConvertToDto(address));
            }

            return addressList;
        }

        public List<EventDto> GetActiveEvents()
        {
            throw new NotImplementedException();
        }

        public List<ScheduleDto> GetActiveSchedules()
        {
            throw new NotImplementedException();
        }

        public List<UserDto> GetActiveUsers()
        {
            throw new NotImplementedException();
        }

        public List<VehicleDto> GetActiveVehicles()
        {
            throw new NotImplementedException();
        }

        public AddressDto GetAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public EventDto GetEventById(int id)
        {
            throw new NotImplementedException();
        }

        public ScheduleDto GetScheduleById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public VehicleDto GetVehicleById(int id)
        {
            throw new NotImplementedException();
        }

        public bool InsertAddress(AddressDto addressModel)
        {
            throw new NotImplementedException();
        }

        public bool InsertEvent(EventDto eventModel)
        {
            throw new NotImplementedException();
        }

        public bool InsertSchedule(ScheduleDto scheduleModel)
        {
            throw new NotImplementedException();
        }

        public bool InsertUser(UserDto userModel)
        {
            throw new NotImplementedException();
        }

        public bool InsertVehicle(VehicleDto vehicleModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAddress(AddressDto addressModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEvent(EventDto eventModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSchedule(ScheduleDto scheduleModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(UserDto userModel)
        {
            throw new NotImplementedException();
        }

        public bool UpdateVehicle(VehicleDto vehicleModel)
        {
            throw new NotImplementedException();
        }
    }
}
