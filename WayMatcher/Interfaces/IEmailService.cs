using WayMatcherBL.DtoModels;

namespace WayMatcherBL.Interfaces
{
    /// <summary>
    /// Defines the contract for email-related operations.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="email">The email DTO containing the email details.</param>
        public void SendEmail(EmailDto email);
    }
}
