using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Text;
using Appointment_booking_system.Models;
using Appointment_booking_system.ExceptionHandling;
using Appointment_booking_system.utils;
using GEAWebAPI.Controllers.Service;
using Microsoft.AspNetCore.Authorization;

namespace Appointment_booking_system.Controllers
{
 
    public class RegisterController : Controller
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register()
        {
            try
            {
                var firstName = Request.Form["FirstName"];
                var lastName = Request.Form["LastName"];
                var email = Request.Form["Email"];
                var civilID = Request.Form["CID"];
                var mobileNo = Request.Form["Number"];

                var usr = _context.Users.FirstOrDefault(x => x.Email == email.ToString());
                if (usr != null)
                {
                    //If the user email is already registered in the system
                    return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_US_005, MessageHandler.getMessage(ErrorCode.AB_US_005)));
                }

                string generatedPassword = createRandomPassword();
                string subject = "Password generated for account";
                string body = $"Dear {firstName} {lastName}" + Environment.NewLine + Environment.NewLine + $"You have successfully registered on the online booking portal. Please use {generatedPassword} to login to the system ";
                EmailUtil emailUtil = new EmailUtil();
                emailUtil.SendEmail(email, subject, body);
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = generatedPassword,
                    CivilId = civilID,
                    MobileNo = mobileNo
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return Json(ControllerReturn<bool>.ReturnSingle(true));
            }
            catch (Exception e)
            {
                Logger.Error(e, ErrorCode.AB_US_004, MessageHandler.getMessage(ErrorCode.AB_US_004));
                return Json(ControllerReturn<bool>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_US_004, e, MessageHandler.getMessage(ErrorCode.AB_US_004)));
            }
        }


        private string createRandomPassword()
        {
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

            return new string(chars);
        }
    }
}
