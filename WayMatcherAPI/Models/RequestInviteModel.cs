using WayMatcherBL.LogicModels;

namespace WayMatcherAPI.Models
{
    public class RequestInviteModel
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public bool IsPilot { get; set; }
    }
}
