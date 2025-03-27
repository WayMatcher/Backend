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
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventController"/> class.
        /// </summary>
        /// <param name="eventService">The event service.</param>
        /// <param name="userService">The user service.</param>
        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="requestEvent">The request event model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("CreateEvent")]
        public IActionResult CreateEvent([FromBody] RequestEvent requestEvent)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.CreateEvent(requestEvent.Event, requestEvent.User);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="requestEvent">The request event model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("UpdateEvent")]
        public IActionResult UpdateEvent([FromBody] RequestEvent requestEvent)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.UpdateEvent(requestEvent.Event);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Deletes an event.
        /// </summary>
        /// <param name="requestEvent">The request event model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("DeleteEvent")]
        public IActionResult DeleteEvent([FromBody] RequestEvent requestEvent)
        {
            return HandleRequest(() =>
            {
                return _eventService.CancelEvent(requestEvent.Event) ? Ok("Event deleted.") : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Requests an event invite.
        /// </summary>
        /// <param name="invite">The request invite model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("RequestEventInvite")]
        public IActionResult RequestEventInvite([FromBody] RequestInviteModel invite)
        {
            return HandleRequest(() =>
            {
                var inviteDto = CreateInviteDto(invite, true);
                return _eventService.EventInvite(inviteDto) ? Ok("Invite sent.") : BadRequest();
            });
        }

        /// <summary>
        /// Sends an event invite.
        /// </summary>
        /// <param name="invite">The request invite model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("SendEventInvite")]
        public IActionResult SendEventInvite([FromBody] RequestInviteModel invite)
        {
            return HandleRequest(() =>
            {
                var inviteDto = CreateInviteDto(invite, false);
                return _eventService.EventInvite(inviteDto) ? Ok("Invite sent.") : BadRequest();
            });
        }

        /// <summary>
        /// Adds a member to an event.
        /// </summary>
        /// <param name="member">The request event member model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("AddEventMember")]
        public IActionResult AddEventMember([FromBody] RequestEventMember member)
        {
            return HandleRequest(() =>
            {
                var eventMemberDto = new EventMemberDto
                {
                    EventId = member.Event.EventId ?? -1,
                    User = member.User,
                    EventRole = EventRole.Passenger,
                };
                return _eventService.AddEventMember(eventMemberDto) ? Ok("Member added.") : BadRequest();
            });
        }

        /// <summary>
        /// Adds a stop to an event.
        /// </summary>
        /// <param name="stop">The request stop model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("AddStop")]
        public IActionResult AddStop([FromBody] RequestStop stop)
        {
            return HandleRequest(() =>
            {
                var stopDto = new StopDto
                {
                    EventId = stop.EventId,
                    StopId = stop.StopId,
                    Address = stop.Address,
                    StopSequenceNumber = stop.StopSequenceNumber
                };

                return _eventService.AddStop(stopDto) ? Ok("Stop added.") : BadRequest();
            });
        }

        /// <summary>
        /// Removes a stop from an event.
        /// </summary>
        /// <param name="stop">The request stop model.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("RemoveStop")]
        public IActionResult RemoveStop([FromBody] RequestStop stop)
        {
            return HandleRequest(() =>
            {
                var stopDto = new StopDto
                {
                    StopId = stop.StopId,
                };

                return _eventService.RemoveStops(stopDto) ? Ok("Stop removed.") : BadRequest();
            });
        }

        /// <summary>
        /// Gets the list of events.
        /// </summary>
        /// <param name="isPilot">The filter to apply to the event list.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetEventList")]
        public IActionResult GetEventList([FromQuery] bool? isPilot)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.GetEventList(isPilot);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Gets the list of events associated with a user.
        /// </summary>
        /// <param name="user">The user DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("GetUserEvent")]
        public IActionResult GetUserEvent([FromBody] UserDto user)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.GetUserEventList(user);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Gets an event.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetEvent")]
        public IActionResult GetEvent([FromQuery] EventDto eventDto)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.GetEvent(eventDto);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
            });
        }

        /// <summary>
        /// Gets the list of users to invite to an event.
        /// </summary>
        /// <param name="eventDto">The event DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("GetUserToInvite")]
        public IActionResult GetUserToInvite([FromQuery] EventDto eventDto)
        {
            return HandleRequest(() =>
            {
                var result = _eventService.GetUserToInvite(eventDto);
                return result != null ? Ok(result) : NotFound("Event not found or invalid input.");
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
        /// Creates an invite DTO from the request invite model.
        /// </summary>
        /// <param name="invite">The request invite model.</param>
        /// <param name="isRequest">Indicates whether the invite is a request.</param>
        /// <returns>The invite DTO.</returns>
        private InviteDto CreateInviteDto(RequestInviteModel invite, bool isRequest)
        {
            var inviteDto = new InviteDto
            {
                EventId = invite.EventId,
                User = _userService.GetUser(new UserDto { UserId = invite.UserId }),
                Message = invite.Message,
                IsRequest = isRequest,
                eventRole = invite.IsPilot ? EventRole.Pilot : EventRole.Passenger
            };
            return inviteDto;
        }
    }
}
