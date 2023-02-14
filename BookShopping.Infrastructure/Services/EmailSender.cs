using System;
using System.Net.Mail;
using BookShopping.Application.Interfaces;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BookShopping.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["MailSettings:Mail"]));
            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = subject;
            var builder = new BodyBuilder();

            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            smtp.Connect(_configuration["MailSettings:Host"],
                            Convert.ToInt32(_configuration["MailSettings:Port"]),
                            SecureSocketOptions.StartTls);

            smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}

