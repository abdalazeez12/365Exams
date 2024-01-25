namespace Imtihani.Services
{
    public interface IEmailService
    {
        public void SendEmail(string body, string to = "admin@365exams.com", string subject = "Contact Us");
    }
}
