using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Kartverket.Services
{

    /// <summary>
    /// Service responsible for sending emails using SMTP configuration.
    /// Implements <see cref="IEmailSender"/> interface for email functionality.
    /// </summary>
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class with configuration settings.
        /// </summary>
        /// <param name="configuration">Configuration settings for email (SMTP server, port, credentials, etc.)</param>
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Sends an email asynchronously with the provided subject and message.
        /// </summary>
        /// <param name="email">Recipient email address</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="message">Body message of the email</param>
        /// <returns>A Task representing the asynchronous operation</returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Create a new SMTP client using the configuration settings
            var smtpClient = new SmtpClient(_configuration["EmailSettings:SmtpServer"])
            {
                Port = int.Parse(_configuration["EmailSettings:Port"]), // Set SMTP port from configuration
                Credentials = new NetworkCredential(
                    _configuration["EmailSettings:Username"], // Set SMTP username from configuration
                    _configuration["EmailSettings:Password"] // Set SMTP password from configuration
                    ),
                EnableSsl = true // Enable SSL for secure email transmission
            };

            // Create a new MailMessage with the specified sender, recipient, subject, and body
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:FromEmail"]), // Sender email from configuration
                Subject = subject, // Set email subject
                Body = message, // Set email body
                IsBodyHtml = true // Set the email body to HTML format
            };

            // Add recipient email to the message
            mailMessage.To.Add(email);

            // Asynchronously send the email using the SMTP client
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}