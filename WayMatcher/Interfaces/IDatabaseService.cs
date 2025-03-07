﻿using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IDatabaseService
    {
        public bool InsertUser(UserDto userModel);
        public bool UpdateUser(UserDto userModel);
        public UserDto GetUserById(int id);
        public List<UserDto> GetActiveUsers();
        public int GetUserId(string username);

        public bool InsertAddress(AddressDto addressModel);
        public bool UpdateAddress(AddressDto addressModel);
        public AddressDto GetAddressById(int id);
        public List<AddressDto> GetActiveAddresses();
        public int GetAddressId(AddressDto addressModel);

        public bool InsertVehicle(VehicleDto vehicleModel);
        public bool UpdateVehicle(VehicleDto vehicleModel);
        public VehicleDto GetVehicleById(int id);
        public List<VehicleDto> GetActiveVehicles();
        public int GetVehicleId(VehicleDto vehicleModel);

        public bool InsertSchedule(ScheduleDto scheduleModel);
        public bool UpdateSchedule(ScheduleDto scheduleModel);
        public ScheduleDto GetScheduleById(int id);
        public List<ScheduleDto> GetActiveSchedules();
        public int GetScheduleId(ScheduleDto scheduleModel);

        public bool InsertEvent(EventDto eventModel);
        public bool UpdateEvent(EventDto eventModel);
        public EventDto GetEventById(int id);
        public List<EventDto> GetActiveEvents();
        public int GetEventId(EventDto eventModel);

        public List<VehicleDto> GetUserVehicles(int userId);
        public bool InsertVehicleMapping(VehicleMappingDto vehicleMapping);
    }
}
