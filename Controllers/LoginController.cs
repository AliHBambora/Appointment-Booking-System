using Appointment_booking_system.ExceptionHandling;
using Appointment_booking_system.Models;
using Appointment_booking_system.utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Appointment_booking_system.Controllers
{
    public class LoginController : Controller
    {

        private readonly DataContext _context;


        public LoginController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login()
        {
            var email = Request.Form["Email"];
            var password = Request.Form["Password"];
            try
            {
                User user = _context.Users.FirstOrDefault(x => x.Email == email.ToString());
                if (user == null)
                {
                    return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_US_001, MessageHandler.getMessage(ErrorCode.AB_US_001)));
                }
                else
                {
                    if (user.Password != password)
                    {
                        return Json(ControllerReturn<string>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_US_002, MessageHandler.getMessage(ErrorCode.AB_US_002)));
                    }
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.FirstName+user.LastName),
                        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),properties);

                    return Json(ControllerReturn<bool>.ReturnSingle(true));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, ErrorCode.AB_US_003, MessageHandler.getMessage(ErrorCode.AB_US_003));
                return Json(ControllerReturn<string>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_US_003, e, MessageHandler.getMessage(ErrorCode.AB_US_003)));
            }
        }
    }
}
