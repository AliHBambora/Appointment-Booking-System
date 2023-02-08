using System.Net.Mail;
using System.Net;

namespace Appointment_booking_system.utils
{
    public class EmailUtil
    {
        public void SendEmail(string receiverEmail,string subject,string body)
        {
            try
            {
                var builder = WebApplication.CreateBuilder();
                MailMessage message = new MailMessage();
                string fromEmail = builder.Configuration.GetSection("Smtp").GetSection("Username").Value;
                string fromPW = builder.Configuration.GetSection("Smtp").GetSection("Password").Value;
                string host = builder.Configuration.GetSection("Smtp").GetSection("Host").Value;
                string toEmail = receiverEmail;
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                using (SmtpClient smtpClient = new SmtpClient(host, 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                    smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                    message.Subject, message.Body);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
