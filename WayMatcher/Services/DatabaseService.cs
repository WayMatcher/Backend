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
            foreach (var address in _dbContext.Addresses.Where(a => a.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                addressList.Add(_mapper.ConvertAddressToDto(address));
            }

            return addressList;
        }

        public List<EventDto> GetActiveEvents()
        {
            var eventList = new List<EventDto>();
            foreach (var eventItem in _dbContext.Events.Where(e => e.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                eventList.Add(_mapper.ConvertEventToDto(eventItem));
            }

            return eventList;
        }

        public List<ScheduleDto> GetActiveSchedules()
        {
            //var scheduleList = new List<ScheduleDto>();
            //foreach (var schedule in _dbContext.Schedules.Where(s => s.Status.StatusDescription.Equals(State.Active)).ToList())
            //{
            //    scheduleList.Add(_mapper.ConvertScheduleToDto(schedule));
            //}

            //return scheduleList;
            return null;
        }

        public List<UserDto> GetActiveUsers()
        {
            var userList = new List<UserDto>();
            foreach (var user in _dbContext.Users.Where(u => u.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                userList.Add(_mapper.ConvertUserToDto(user));
            }

            return userList;
        }

        public List<VehicleDto> GetActiveVehicles()
        {
            var vehicleList = new List<VehicleDto>();
            foreach (var vehicle in _dbContext.Vehicles.Where(v => v.Status.StatusDescription.Equals(State.Active.GetDescription())).ToList())
            {
                vehicleList.Add(_mapper.ConvertVehicleToDto(vehicle));
            }

            return vehicleList;
        }

        public AddressDto GetAddressById(int id)
        {
            var address = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == id);
            if (address == null)
            {
                return null;
            }
            return _mapper.ConvertAddressToDto(address);
        }

        public EventDto GetEventById(int id)
        {
            var eventItem = _dbContext.Events.FirstOrDefault(e => e.EventId == id);
            if (eventItem == null)
            {
                return null;
            }
            return _mapper.ConvertEventToDto(eventItem);
        }

        public ScheduleDto GetScheduleById(int id)
        {
            var schedule = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == id);
            if (schedule == null)
            {
                return null;
            }
            return _mapper.ConvertScheduleToDto(schedule);
        }

        public UserDto GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == id);
            if (user == null)
            {
                return null;
            }
            return _mapper.ConvertUserToDto(user);
        }

        public VehicleDto GetVehicleById(int id)
        {
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == id);
            if (vehicle == null)
            {
                return null;
            }
            return _mapper.ConvertVehicleToDto(vehicle);
        }

        public int GetAddressId(AddressDto addressModel)
        {
            var address = _dbContext.Addresses.FirstOrDefault(a => a.Longitude == addressModel.Longitude && a.Latitude == addressModel.Latitude && a.City == addressModel.City && a.PostalCode == addressModel.PostalCode && a.Country == addressModel.Country);
            if (address == null)
            {
                return -1;
            }
            return address.AddressId;
        }

        public int GetEventId(EventDto eventModel)
        {
            var eventItem = _dbContext.Events.FirstOrDefault(e => e.Description == eventModel.Description && e.EventTypeId == eventModel.EventTypeId && e.FreeSeats == eventModel.FreeSeats && e.StartTimestamp == eventModel.StartTimestamp);
            if (eventItem == null)
            {
                return -1;
            }
            return eventItem.EventId;
        }

        public int GetScheduleId(ScheduleDto scheduleModel)
        {
            var schedule = _dbContext.Schedules.FirstOrDefault(s => s.CronSchedule == scheduleModel.CronSchedule);
            if (schedule == null)
            {
                return -1;
            }
            return schedule.ScheduleId;
        }

        public int GetUserId(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                return -1;
            }
            return user.UserId;
        }

        public int GetVehicleId(VehicleDto vehicleModel)
        {
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.Model == vehicleModel.Model && v.Seats == vehicleModel.Seats);
            if (vehicle == null)
            {
                return -1;
            }
            return vehicle.VehicleId;
        }

        public bool InsertAddress(AddressDto addressModel)
        {
            var addressEntity = _mapper.ConvertAddressDtoToEntity(addressModel);
            
            addressEntity.Status = new Status();
            addressEntity.Status.StatusDescription = State.Active.GetDescription();

            _dbContext.Addresses.Add(addressEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertEvent(EventDto eventModel)
        {
            var eventEntity = _mapper.ConvertEventDtoToEntity(eventModel);

            eventEntity.Status = new Status();
            eventEntity.Status.StatusDescription = State.Active.GetDescription();

            _dbContext.Events.Add(eventEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertSchedule(ScheduleDto scheduleModel)
        {
            var scheduleEntity = _mapper.ConvertScheduleDtoToEntity(scheduleModel);
            _dbContext.Schedules.Add(scheduleEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertUser(UserDto userModel)
        {
            var userEntity = _mapper.ConvertUserDtoToEntity(userModel);

            userEntity.Status = new Status();
            userEntity.Status.StatusDescription = State.Active.GetDescription();

            _dbContext.Users.Add(userEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertVehicle(VehicleDto vehicleModel)
        {
            var vehicleEntity = _mapper.ConvertVehicleDtoToEntity(vehicleModel);

            vehicleEntity.Status = new Status();
            vehicleEntity.Status.StatusDescription = State.Active.GetDescription();

            _dbContext.Vehicles.Add(vehicleEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateAddress(AddressDto addressModel)
        {
            var addressEntity = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == addressModel.AddressId);
            if (addressEntity == null)
            {
                return false;
            }

            addressEntity.City = addressModel.City;
            addressEntity.PostalCode = addressModel.PostalCode;
            addressEntity.Street = addressModel.Street;
            addressEntity.Country = addressModel.Country;
            addressEntity.CountryCode = addressModel.CountryCode;
            addressEntity.Region = addressModel.Region;
            addressEntity.State = addressModel.State;
            addressEntity.Longitude = addressModel.Longitude;
            addressEntity.Latitude = addressModel.Latitude;
            addressEntity.AddressLine1 = addressModel.AddressLine1;
            addressEntity.AddressLine2 = addressModel.AddressLine2;
            addressEntity.StatusId = addressModel.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateEvent(EventDto eventModel)
        {
            var eventEntity = _dbContext.Events.FirstOrDefault(e => e.EventId == eventModel.EventId);
            if (eventEntity == null)
            {
                return false;
            }

            eventEntity.EventTypeId = eventModel.EventTypeId;
            eventEntity.FreeSeats = eventModel.FreeSeats;
            eventEntity.Description = eventModel.Description;
            eventEntity.StartTimestamp = eventModel.StartTimestamp;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateSchedule(ScheduleDto scheduleModel)
        {
            var scheduleEntity = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleModel.ScheduleId);
            if (scheduleEntity == null)
            {
                return false;
            }

            scheduleEntity.CronSchedule = scheduleModel.CronSchedule;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateUser(UserDto userModel)
        {
            var userEntity = _dbContext.Users.FirstOrDefault(u => u.UserId == userModel.UserId);
            if (userEntity == null)
            {
                return false;
            }

            userEntity.Firstname = userModel.Firstname;
            userEntity.Name = userModel.Name;
            userEntity.Username = userModel.Username;
            userEntity.Password = userModel.Password;
            userEntity.EMail = userModel.EMail;
            userEntity.Telephone = userModel.Telephone;
            userEntity.AdditionalDescription = userModel.AdditionalDescription;
            userEntity.LicenseVerified = userModel.LicenseVerified;
            userEntity.ProfilePicture = userModel.ProfilePicture;
            userEntity.CreationDate = userModel.CreationDate;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateVehicle(VehicleDto vehicleModel)
        {
            var vehicleEntity = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleModel.VehicleId);
            if (vehicleEntity == null)
            {
                return false;
            }

            vehicleEntity.Model = vehicleModel.Model;
            vehicleEntity.Seats = vehicleModel.Seats;
            vehicleEntity.YearOfManufacture = vehicleModel.YearOfManufacture;
            vehicleEntity.ManufacturerName = vehicleModel.ManufacturerName;

            return _dbContext.SaveChanges() > 0;
        }
    }
}
