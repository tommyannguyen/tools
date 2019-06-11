using Hangfire;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Nca.Library.Interfaces;
using System;
using System.Threading.Tasks;

namespace Nca.Library.Models.Repositories
{
    public class EmailSenderJob : IJob, IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailSenderJob(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        [AutomaticRetry(Attempts = 5)]
        public async Task SendEmailJobAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
                mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);
                    await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
                    await client.SendAsync(mimeMessage);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            BackgroundJob.Enqueue<EmailSenderJob>( jobs => jobs.SendEmailJobAsync(email, subject, message)); 
        }
    }
}
