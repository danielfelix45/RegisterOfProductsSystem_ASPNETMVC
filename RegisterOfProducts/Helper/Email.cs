using System.Net.Mail;
using System.Net;

namespace RegisterOfProducts.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Sent(string email, string topic, string message)
        {
            try
            {
                string host = _configuration.GetValue<string>("SMTP:Host");
                string name = _configuration.GetValue<string>("SMTP:Nome");
                string username = _configuration.GetValue<string>("SMTP:UserName");
                string password = _configuration.GetValue<string>("SMTP:Senha");
                int port = _configuration.GetValue<int>("SMTP:Porta");

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(username, name)
                };

                mail.To.Add(email);
                mail.Subject = topic;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(host, port))
                {
                    smtp.Credentials = new NetworkCredential(username, password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
