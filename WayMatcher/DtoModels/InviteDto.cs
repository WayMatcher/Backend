using WayMatcherBL.Enums;
using WayMatcherBL.LogicModels;

namespace WayMatcherBL.DtoModels
{
    public class InviteDto
    {
        public int InviteId { get; set; }

        public int? ConfirmationStatusId { get; set; }
        
        public bool IsRequest { get; set; }
        
        public EventRole eventRole { get; set; }
        
        public int? EventId { get; set; }
        
        public UserDto? User { get; set; }
        
        public string? Message { get; set; }
    }
}
