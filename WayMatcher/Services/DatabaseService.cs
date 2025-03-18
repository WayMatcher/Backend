using WayMatcherBL.DtoModels;
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
        public List<EventDto> GetFilteredEventList() //wird mithilfe von Views gefiltert #TODO
        {
            //var filteredEventList = new List<EventDto>();
            //foreach(var eventEntity in _dbContext.Events.Where(e => e.Schedule == )))
            return null;
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

        public EventDto GetEvent(EventDto eventDto)
        {
            var eventItem = _dbContext.Events.FirstOrDefault(e => e.EventId == eventDto.EventId);
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

        public UserDto GetUser(UserDto userDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.EMail == userDto.Email || u.Username == userDto.Username || u.UserId == userDto.UserId);
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

        public List<VehicleDto> GetUserVehicles(int userId)
        {
            var vehicleList = new List<VehicleDto>();
            var vehicles = _dbContext.VehicleMappings.Where(vm => vm.UserId == userId).Select(vm => vm.Vehicle).ToList();

            foreach (var vehicle in vehicles)
            {
                vehicleList.Add(_mapper.ConvertVehicleToDto(vehicle));
            }

            return vehicleList;
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
        
        public List<StopDto> GetStopList(EventDto eventDto)
        {
            var stopList = new List<StopDto>();
            var stops = _dbContext.Stops.Where(s => s.EventId == eventDto.EventId).ToList(); 

            foreach (var stop in stops)
            {
                stopList.Add(_mapper.ConvertStopToDto(stop));
            }

            return stopList;
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

        public bool InsertVehicleMapping(VehicleMappingDto vehicleMapping)
        {
            var vehicleMappingEntity = _mapper.ConvertVehicleMappingDtoToEntity(vehicleMapping);

            vehicleMappingEntity.Status = new Status();
            vehicleMappingEntity.Status.StatusDescription = State.Active.GetDescription();

            _dbContext.VehicleMappings.Add(vehicleMappingEntity);
            return _dbContext.SaveChanges() > 0;
        }
        public bool InsertStop(StopDto stop)
        {
            var stopEntity = _mapper.ConvertStopDtoToEntity(stop);

            _dbContext.Stops.Add(stopEntity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertToInvite(InviteDto invite)
        {
            var inviteEntity = _mapper.ConvertDtoToInvite(invite);

            _dbContext.Invites.Add(inviteEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool InsertToEventMember(EventMemberDto eventMember)
        {
            var eventMemberEntity = _mapper.ConvertEventMemberDtoToEntity(eventMember);

            _dbContext.EventMembers.Add(eventMemberEntity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateAddress(AddressDto addressModel)
        {
            var addressEntity = _dbContext.Addresses.FirstOrDefault(a => a.AddressId == addressModel.AddressId);
            if (addressEntity == null)
                return false;

            if (!string.IsNullOrEmpty(addressModel.City))
                addressEntity.City = addressModel.City;

            if (!string.IsNullOrEmpty(addressModel.PostalCode))
                addressEntity.PostalCode = addressModel.PostalCode;

            if (!string.IsNullOrEmpty(addressModel.Street))
                addressEntity.Street = addressModel.Street;

            if (!string.IsNullOrEmpty(addressModel.Country))
                addressEntity.Country = addressModel.Country;

            if (!string.IsNullOrEmpty(addressModel.CountryCode))
                addressEntity.CountryCode = addressModel.CountryCode;

            if (!string.IsNullOrEmpty(addressModel.Region))
                addressEntity.Region = addressModel.Region;

            if (!string.IsNullOrEmpty(addressModel.State))
                addressEntity.State = addressModel.State;

            if (addressModel.Longitude.HasValue)
                addressEntity.Longitude = addressModel.Longitude;

            if (addressModel.Latitude.HasValue)
                addressEntity.Latitude = addressModel.Latitude;

            if (!string.IsNullOrEmpty(addressModel.AddressLine1))
                addressEntity.AddressLine1 = addressModel.AddressLine1;

            if (!string.IsNullOrEmpty(addressModel.AddressLine2))
                addressEntity.AddressLine2 = addressModel.AddressLine2;

            if (addressModel.StatusId.HasValue)
                addressEntity.StatusId = addressModel.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateEvent(EventDto eventModel)
        {
            var eventEntity = _dbContext.Events.FirstOrDefault(e => e.EventId == eventModel.EventId);
            if (eventEntity == null)
                return false;

            if (eventModel.EventTypeId.HasValue)
                eventEntity.EventTypeId = eventModel.EventTypeId;

            if (eventModel.FreeSeats.HasValue)
                eventEntity.FreeSeats = eventModel.FreeSeats;

            if (!string.IsNullOrEmpty(eventModel.Description))
                eventEntity.Description = eventModel.Description;

            if (eventModel.StartTimestamp.HasValue)
                eventEntity.StartTimestamp = eventModel.StartTimestamp;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateSchedule(ScheduleDto scheduleModel)
        {
            var scheduleEntity = _dbContext.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleModel.ScheduleId);
            if (scheduleEntity == null)
                return false;

            if (!string.IsNullOrEmpty(scheduleModel.CronSchedule))
                scheduleEntity.CronSchedule = scheduleModel.CronSchedule;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateUser(UserDto userModel)
        {
            var userEntity = _dbContext.Users.FirstOrDefault(u => u.UserId == userModel.UserId);
            if (userEntity == null)
                return false;

            if (!string.IsNullOrEmpty(userModel.Firstname))
                userEntity.Firstname = userModel.Firstname;

            if (!string.IsNullOrEmpty(userModel.Name))
                userEntity.Name = userModel.Name;

            if (!string.IsNullOrEmpty(userModel.Username))
                userEntity.Username = userModel.Username;

            if (!string.IsNullOrEmpty(userModel.Password))
                userEntity.Password = userModel.Password;

            if (!string.IsNullOrEmpty(userModel.Email))
                userEntity.EMail = userModel.Email;

            if (!string.IsNullOrEmpty(userModel.Telephone))
                userEntity.Telephone = userModel.Telephone;

            if (!string.IsNullOrEmpty(userModel.AdditionalDescription))
                userEntity.AdditionalDescription = userModel.AdditionalDescription;

            if (userModel.LicenseVerified.HasValue)
                userEntity.LicenseVerified = userModel.LicenseVerified;

            if (userModel.ProfilePicture != null && userModel.ProfilePicture.Length > 0)
                userEntity.ProfilePicture = userModel.ProfilePicture;

            if (userModel.CreationDate.HasValue)
                userEntity.CreationDate = userModel.CreationDate;

            if (!string.IsNullOrEmpty(userModel.MfAtoken))
                userEntity.MfAtoken = userModel.MfAtoken;

            if (userModel.StatusId.HasValue)
                userEntity.StatusId = userModel.StatusId;

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateVehicle(VehicleDto vehicleModel)
        {
            var vehicleEntity = _dbContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleModel.VehicleId);
            if (vehicleEntity == null)
                return false;

            if (!string.IsNullOrEmpty(vehicleModel.Model))
                vehicleEntity.Model = vehicleModel.Model;

            if (vehicleModel.Seats.HasValue)
                vehicleEntity.Seats = vehicleModel.Seats;

            if (vehicleModel.YearOfManufacture.HasValue)
                vehicleEntity.YearOfManufacture = vehicleModel.YearOfManufacture;

            if (!string.IsNullOrEmpty(vehicleModel.ManufacturerName))
                vehicleEntity.ManufacturerName = vehicleModel.ManufacturerName;

            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteStop(StopDto stop)
        {
            var stopEntity = _dbContext.Stops.FirstOrDefault(s => s.StopId == stop.StopId);

            if(stopEntity == null)
                return false;

            _dbContext.Stops.Remove(stopEntity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteEventMember(EventMemberDto eventMember)
        {
            throw new NotImplementedException();
        }

        public List<EventMemberDto> GetEventMemberList(EventDto eventDto)
        {
            var eventMemberList = new List<EventMemberDto>();
            var eventMembers = _dbContext.EventMembers.Where(em => em.EventId == eventDto.EventId).ToList();

            foreach(var eventMember in eventMembers)
            {
                eventMemberList.Add(_mapper.ConvertEventMemberToDto(eventMember));
            }

            return eventMemberList;
        }
    }
}
