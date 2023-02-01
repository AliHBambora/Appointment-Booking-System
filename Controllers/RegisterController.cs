using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace Appointment_booking_system.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register()
        {
            var name = Request.Form["Name"];
            var email = Request.Form["Email"];
            var civilID = Request.Form["CID"];
            var mobileNo = Request.Form["Number"];

            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[10];
            for (int i = 0; i < 10; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            string generatedPassword = new string(chars);
            MailMessage message = new MailMessage();
            string fromEmail = "bamah036@gmail.com";
            string fromPW = "anadktksnfbvyupy";
            string toEmail = "alihussainb67@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Password generated for account";
            message.Body = $"Dear {name}" + Environment.NewLine + Environment.NewLine + "You have successfully registered on the online booking portal. Please use {generatedPassword.} to login to the system ";
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View();
        }
    }
}
