﻿using WayMatcherBL.DtoModels;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.Interfaces
{
    public interface IDatabaseService
    {
        public bool InsertUser(UserDto userModel);
        public bool UpdateUser(UserDto userModel);
        public UserDto GetUser(UserDto user);
        public List<UserDto> GetActiveUsers();

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
        public List<ScheduleDto> GetUserSchedules(UserDto user);
        public int GetScheduleId(ScheduleDto scheduleModel);

        public bool InsertEvent(EventDto eventModel);
        public bool UpdateEvent(EventDto eventModel);
        public EventDto GetEvent(EventDto eventDto);
        public List<EventDto> GetFilteredEventList(FilterDto filter);
        public int GetEventId(EventDto eventModel);

        public List<VehicleDto> GetUserVehicles(int userId);
        public bool InsertVehicleMapping(VehicleMappingDto vehicleMapping);

        public bool InsertStop(StopDto stop);
        public bool DeleteStop(StopDto stop);
        public List<StopDto> GetStopList(EventDto eventDto);

        public bool InsertToInvite(InviteDto invite);

        public bool InsertToEventMember(EventMemberDto eventMember);
        public bool UpdateEventMember(EventMemberDto eventMember);
        public List<EventMemberDto> GetEventMemberList(EventDto eventDto);

        public bool InsertChatMessage(ChatMessageDto textMessage);
        public List<ChatMessageDto> GetChatMessageList(EventMemberDto eventMember);

        public bool InsertRating(RatingDto rating);
        public bool UpdateRating(RatingDto rating);
        public List<RatingDto> GetRatingList(UserDto user);
        public RatingDto GetRating(RatingDto rating);
    }
}
