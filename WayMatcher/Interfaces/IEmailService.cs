using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto email);
        
    }
}
