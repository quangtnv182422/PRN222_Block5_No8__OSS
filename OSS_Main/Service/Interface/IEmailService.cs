namespace OSS_Main.Service.Interface
{
    public interface IEmailService
    {
        public Task SendWelcomeEmail(string email, string username, string password);
    }
}
