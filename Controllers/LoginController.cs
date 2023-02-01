using Microsoft.AspNetCore.Mvc;

namespace Appointment_booking_system.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            var email = Request.Form["Email"];
            var password = Request.Form["Password"];
            return View();
        }
    }
}
