namespace WayMatcherBL.LogicModels
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string EMail { get; set; }

        public string Telephone { get; set; }

        public string AdditionalDescription { get; set; }

        public bool? LicenseVerified { get; set; }

        public byte[] ProfilePicture { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
