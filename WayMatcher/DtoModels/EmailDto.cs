namespace WayMatcherBL.DtoModels
{
    public class EmailDto
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public bool IsHtml { get; set; }
    }
}
