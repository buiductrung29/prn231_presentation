using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Identity_Sample.Mail
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
    public class SendMailService : IEmailSender
    {
        private readonly MailSettings mailSettings;
        private readonly ILogger<SendMailService> logger;
        public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
        {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            // logger.LogInformation("Create Send Mail Service");
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlMessage;
            message.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(message);
            }
            catch (Exception e)
            {
                System.IO.Directory.CreateDirectory("MailsSave");
                var _emailSaveFile = string.Format(@"MailsSave/{0}.eml", Guid.NewGuid());
                await message.WriteToAsync(_emailSaveFile);

                logger.LogInformation("Error send mail, save at " + _emailSaveFile);
                logger.LogError(e.Message);
            }
            smtp.Disconnect(true);
            logger.LogInformation("Send Email to: " + email);
        }
    }
}