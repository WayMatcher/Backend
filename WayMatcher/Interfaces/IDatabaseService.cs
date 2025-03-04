using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IDatabaseService
    {
        public bool InsertUser(UserDto userModel);
        public bool UpdateUser(UserDto userModel);
        public UserDto GetUserById(int id);
        public List<UserDto> GetActiveUsers();

        public bool InsertAddress(AddressDto addressModel);
        public bool UpdateAddress(AddressDto addressModel);
        public AddressDto GetAddressById(int id);
        public List<AddressDto> GetActiveAddresses();

        public bool InsertVehicle(VehicleDto vehicleModel);
        public bool UpdateVehicle(VehicleDto vehicleModel);
        public VehicleDto GetVehicleById(int id);
        public List<VehicleDto> GetActiveVehicles();

        public bool InsertSchedule(ScheduleDto scheduleModel);
        public bool UpdateSchedule(ScheduleDto scheduleModel);
        public ScheduleDto GetScheduleById(int id);
        public List<ScheduleDto> GetActiveSchedules();

        public bool InsertEvent(EventDto eventModel);
        public bool UpdateEvent(EventDto eventModel);
        public EventDto GetEventById(int id);
        public List<EventDto> GetActiveEvents();
    }
}
