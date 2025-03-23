using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.DtoModels;
using WayMatcherBL.Enums;
using WayMatcherBL.Interfaces;
using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Controllers
{
    [Route("api/Event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("CreateEvent")]
        public IActionResult CreateEvent([FromBody] RequestEvent requestEvent)
        {
            try
            {
                var result = _eventService.CreateEvent(requestEvent.Event, requestEvent.StopList, requestEvent.User, requestEvent.Schedule);

                if (result != null)
                    return Ok(result); //returns event
                else
                    return NotFound("Event not found or invalid input.");
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

        [HttpPost("UpdateEvent")]
        public IActionResult UpdateEvent([FromBody] RequestEvent requestEvent)
        {
            try
            {
                var result = _eventService.UpdateEvent(requestEvent.Event, requestEvent.Schedule);
                if (result != null)
                    return Ok(result); //returns event
                else
                    return NotFound("Event not found or invalid input.");
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

        [HttpPost("DeleteEvent")]
        public IActionResult DeleteEvent([FromBody] RequestEvent requestEvent)
        {
            try
            {
                if (_eventService.CancelEvent(requestEvent.Event))
                    return Ok("Event deleted.");
                else
                    return NotFound("Event not found or invalid input.");
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

        [HttpPost("RequestEventInvite")]
        public IActionResult RequestEventInvite([FromBody] RequestInviteModel invite)
        {
            try
            {
                InviteDto inviteDto = new InviteDto()
                {
                    EventId = invite.Event.EventId,
                    UserId = invite.User.UserId,
                    IsRequest = true
                };

                if (_eventService.EventInvite(inviteDto))
                    return Ok("Invite sent.");
                else
                    return BadRequest();
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

        [HttpPost("SendEventInvite")]
        public IActionResult SendEventInvite([FromBody] RequestInviteModel invite)
        {
            try
            {
                InviteDto inviteDto = new InviteDto()
                {
                    EventId = invite.Event.EventId,
                    UserId = invite.User.UserId,
                    IsRequest = false
                };
                if (_eventService.EventInvite(inviteDto))
                    return Ok("Invite sent.");
                else
                    return BadRequest();
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

        [HttpPost("AddEventMember")] //happens over email link -> to page where this is getting called
        public IActionResult AddEventMember([FromBody] RequestEventMember member)
        {
            try
            {
                EventMemberDto eventMemberDto = new EventMemberDto()
                {
                    EventId = member.Event.EventId,
                    UserId = member.User.UserId,
                    EventRole = (int)EventRole.Passenger,
                };
                if (_eventService.AddEventMember(eventMemberDto))
                    return Ok("Member added.");
                else
                    return BadRequest();
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

        [HttpPost("AddStop")]
        public IActionResult AddStop([FromBody] RequestStop stop)
        {
            try
            {
                StopDto stopDto = new StopDto()
                {
                    EventId = stop.EventId,
                    StopId = stop.StopId,
                    Address = stop.Address,
                    StopSequenceNumber = stop.StopSequenceNumber
                };

                if (_eventService.AddStop(stopDto))
                    return Ok("Stop added.");
                else
                    return BadRequest();
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

        [HttpPost("RemoveStop")]
        public IActionResult RemoveStop([FromBody] RequestStop stop)
        {
            try
            {
                StopDto stopDto = new StopDto()
                {
                    StopId = stop.StopId,
                };

                if (_eventService.RemoveStops(stopDto))
                    return Ok("Stop removed.");
                else
                    return BadRequest();
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

        [HttpGet("GetEventList")]
        public IActionResult GetEventList([FromQuery] bool? isPilot)
        {
            try
            {
                var result = _eventService.GetEventList(isPilot);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Event not found or invalid input.");
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
        [HttpPost("GetUserEvent")]
        public IActionResult GetUserEvent([FromBody] UserDto user)
        {
            try
            {
                var result = _eventService.GetUserEventList(user);
                if (result != null)
                    return Ok(result); 
                else
                    return NotFound("Event not found or invalid input.");
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
        [HttpGet("GetEvent")]
        public IActionResult GetEvent([FromQuery] EventDto eventDto)
        {
            try
            {
                var result = _eventService.GetEvent(eventDto);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("Event not found or invalid input.");
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
