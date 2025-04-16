using Microsoft.AspNetCore.Identity.UI.Services;
using OSS_Main.Service.Interface;

namespace OSS_Main.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IEmailSender _emailSender;
        public EmailService(IEmailSender emailSender)
        {
            if (emailSender == null)
            {
                throw new ArgumentNullException(nameof(emailSender), "IEmailSender is null!");
            }
            _emailSender = emailSender;
        }

        public async Task SendWelcomeEmail(string email, string username, string password)
        {
            Console.WriteLine("\nSending welcome email to " + email + "\n");
            string subject = "Welcome to Our Platform!";
            string message = $@"
                <h3>Hello {username},</h3>
                <p>Your account has been successfully created.</p>
                <p><strong>Email:</strong> {email}</p>
                <p><strong>Username:</strong> {username}</p>
                <p><strong>Password:</strong> {password}</p>
                <p>Please change your password after logging in.</p>
                <p>Best regards,</p>
                <p>Your Company Name</p>
            ~
        ";

            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}
