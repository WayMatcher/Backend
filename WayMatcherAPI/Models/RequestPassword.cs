namespace WayMatcherAPI.Models
{
    public class RequestPassword
    {
        public string HashedUsername { get; set; }
        public string Password { get; set; }
    }
}
