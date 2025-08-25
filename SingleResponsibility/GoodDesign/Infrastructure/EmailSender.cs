using SingleResponsibility.GoodDesign.Interfaces;
using System.Net;
using System.Net.Mail;

namespace SingleResponsibility.GoodDesign.Infrastructure
{
    // Notification responsibility

    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailSender(SmtpSettings smtpSettings) => _smtpSettings = smtpSettings;

        // Responsibility 4: Notifications/Email
        public void SendEmail(string subject, string body, string to)
        {


            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                client.EnableSsl = _smtpSettings.EnableSsl;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                // Send Email
                var mail = new MailMessage()
                {
                    To = { to },
                    From = new MailAddress(_smtpSettings.Username, _smtpSettings.From), // Optionally set a friendly display name
                    Subject = subject,
                    Body = body
                };

                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error sending email: " + ex.Message, ex);
                }
            }
        }
    }
}
