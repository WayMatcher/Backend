using Microsoft.AspNetCore.Mvc;
using WayMatcherAPI.Models;
using WayMatcherBL.DtoModels;
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

        [HttpPost("RequestEventInvite")]
        public IActionResult RequestEventInvite([FromBody] RequestInviteModel invite)
        {
            InviteDto inviteDto = new InviteDto()
            {
                EventId = invite.Event.EventId,
                UserId = invite.User.UserId,
                IsRequest = true
            };

            _eventService.EventInvite(inviteDto);

            return BadRequest();
        }
        [HttpPost("SendEventInvite")]
        public IActionResult SendEventInvite([FromBody] RequestInviteModel invite)
        {
            InviteDto inviteDto = new InviteDto()
            {
                EventId = invite.Event.EventId,
                UserId = invite.User.UserId,
                IsRequest = false
            };

            _eventService.EventInvite(inviteDto);

            return BadRequest();
        }
    }
}
