using Appointment_booking_system.ExceptionHandling;
using Appointment_booking_system.Models;
using Appointment_booking_system.utils;
using GEAWebAPI.Controllers.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Appointment_booking_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Json(ControllerReturn<bool>.ReturnSingle(true));
            }
            catch(Exception e)
            {
                return Json(ControllerReturn<string>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_US_006, e, MessageHandler.getMessage(ErrorCode.AB_US_006)));
            }
        }
    }
}