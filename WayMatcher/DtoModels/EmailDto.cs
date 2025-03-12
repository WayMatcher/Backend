namespace WayMatcherBL.DtoModels
{
    public class EmailDto
    {
        public string Username { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}
