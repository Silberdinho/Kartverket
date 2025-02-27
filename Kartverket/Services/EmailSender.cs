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
            string smtpServer = _configuration["SMTP_SERVER"];
            string smtpPortRaw = _configuration["SMTP_PORT"];
            string smtpUsername = _configuration["SMTP_USERNAME"];
            string smtpPassword = _configuration["SMTP_PASSWORD"];
            string smtpFromEmail = _configuration["SMTP_FROM_EMAIL"];

            // Debugging logs
            Console.WriteLine($"SMTP_SERVER: {smtpServer}");
            Console.WriteLine($"SMTP_PORT: {smtpPortRaw}");
            Console.WriteLine($"SMTP_USERNAME: {smtpUsername}");
            Console.WriteLine($"SMTP_PASSWORD: {smtpPassword}");
            Console.WriteLine($"SMTP_FROM_EMAIL: {smtpFromEmail}");

            if (!int.TryParse(smtpPortRaw, out int smtpPort))
            {
                throw new Exception($"Invalid SMTP_PORT value: {smtpPortRaw}");
            }

            var smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpFromEmail),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

    }

    }