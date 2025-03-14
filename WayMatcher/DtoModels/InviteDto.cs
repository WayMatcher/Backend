namespace WayMatcherBL.DtoModels
{
    public class InviteDto
    {
        public int InviteId { get; set; }

        public int? ConfirmationStatusId { get; set; }

        public bool? IsRequest { get; set; }

        public int? EventId { get; set; }

        public int? UserId { get; set; }
    }
}
