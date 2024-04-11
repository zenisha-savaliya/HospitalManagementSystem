using Microsoft.Extensions.Configuration;
using Service.Interface;
using System.Net;
using System.Net.Mail;

namespace Service.Service
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region SendMailMethod

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = _configuration["EmailSettings:MailSettings:Server"];
                    client.Port = int.Parse(_configuration["EmailSettings:MailSettings:Port"]);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(
                        _configuration["EmailSettings:MailSettings:SenderEmail"],
                        _configuration["EmailSettings:MailSettings:Password"]
                    );

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_configuration["EmailSettings:MailSettings:SenderEmail"]);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        await client.SendMailAsync(mailMessage);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
        #endregion

    }
}

