using Appointment_booking_system.DTO;
using Appointment_booking_system.ExceptionHandling;
using Appointment_booking_system.Models;
using Appointment_booking_system.utils;
using GEAWebAPI.Controllers.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Appointment_booking_system.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {

        private readonly DataContext _context;

        public AppointmentController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            try
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Appointment app =  _context.Appointments.FirstOrDefault(x => x.UserId == new Guid(userID));
                if (app != null)
                {
                    ViewBag.userName = User.FindFirstValue(ClaimTypes.Name);
                    ViewBag.isBookingDone = true;
                    ViewBag.bookingDate = app.BookingDate.ToString();
                    return View(app);
                }
                else
                {
                    ViewBag.isBookingDone = false;
                    return View();
                }
            }
            catch(Exception e)
            {
                Logger.Error(e, ErrorCode.AB_BK_002, MessageHandler.getMessage(ErrorCode.AB_BK_002));
                return Json(ControllerReturn<bool>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_002, e, MessageHandler.getMessage(ErrorCode.AB_BK_002)));
            }
        }

        //Controller for booking an apppointment in the system
        [HttpPost]
        public ActionResult Book(AppointmentDTO appointment)
        {
            try
            {
                //If the booking time is less than the current time of the server then throw an error
                if(DateTime.Parse(appointment.BookingDate + " " + appointment.BookingTime) < DateTime.Now)
                {
                    return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_006, MessageHandler.getMessage(ErrorCode.AB_BK_006)));
                }
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.FirstOrDefault(x => x.Id == new Guid(userID));
                if (user != null)
                {
                    if (_context.Appointments.FirstOrDefault(x => x.UserId == user.Id) != null)
                    {
                        //If the booking already exists for the user
                        return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_007, MessageHandler.getMessage(ErrorCode.AB_BK_007)));
                    }
                    Appointment app = new Appointment
                    {
                        Id = Guid.NewGuid(),
                        UserId = new Guid(userID),
                        BookingMonth = int.Parse(DateTime.ParseExact(appointment.BookingDate, "dd/MM/yyyy", CultureInfo.CurrentCulture).Month.ToString()),
                        BookingYear = int.Parse(DateTime.ParseExact(appointment.BookingDate, "dd/MM/yyyy", CultureInfo.CurrentCulture).Year.ToString()),
                        BookingDay = int.Parse(DateTime.ParseExact(appointment.BookingDate, "dd/MM/yyyy", CultureInfo.CurrentCulture).Day.ToString()),
                        BookingDate = DateTime.Parse(appointment.BookingDate+" "+appointment.BookingTime),
                        BookingTime = appointment.BookingTime
                    };
                    _context.Appointments.Add(app);
                    _context.SaveChanges();
                    EmailUtil emailUtil = new EmailUtil();
                    string subject = "Booking created successfully";
                    string body = $"Dear {user.FirstName} {user.LastName}" + Environment.NewLine + Environment.NewLine + $"Congrautulations! Your booking has been successfully registered in the system on {appointment.BookingDate}." + Environment.NewLine + Environment.NewLine + "Thank you for booking with us";
                    emailUtil.SendEmail(user.Email, subject, body);
                    ViewBag.Username = user.FirstName+" "+user.LastName;
                    ViewBag.isBookingDone = true;
                    ViewBag.bookingDate = app.BookingDate+" "+app.BookingTime;
                    return Json(ControllerReturn<bool>.ReturnSingle(true));
                }
                else
                {
                    return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_001, MessageHandler.getMessage(ErrorCode.AB_BK_001)));
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, ErrorCode.AB_BK_001, MessageHandler.getMessage(ErrorCode.AB_BK_001));
                return Json(ControllerReturn<bool>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_001, e, MessageHandler.getMessage(ErrorCode.AB_BK_001)));
            }
        }
        
        //Controller to cancle the booking of the user if the appointment still has more than 24 hrs left.
        [HttpPost]
        public ActionResult CancleBooking()
        {
            try
            {
                var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Appointment app = _context.Appointments.FirstOrDefault(x => x.UserId == new Guid(userID)); //Find the appointment of the current logged in user
                if ((app.BookingDate - DateTime.Now).TotalHours <= 24)
                {
                    //If the appointment has less than 24 hrs left then dont allow the user to cancle the booking
                    return Json(ControllerReturn<bool>.ReturnErrorMessage(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_003, MessageHandler.getMessage(ErrorCode.AB_BK_003)));
                }
                else
                {
                    _context.Appointments.Remove(app);
                    _context.SaveChanges();
                    ViewBag.isBookingDone = false;
                    return Json(ControllerReturn<bool>.ReturnSingle(true));
                }

            }
            catch (Exception e)
            {
                Logger.Error(e, ErrorCode.AB_BK_004, MessageHandler.getMessage(ErrorCode.AB_BK_004));
                return Json(ControllerReturn<bool>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_004,e, MessageHandler.getMessage(ErrorCode.AB_BK_004)));
            }
        }


        //Controller for gettting the booked slots for a particular date
        [HttpPost]
        public ActionResult Get()
        {
            try
            {
                var date = Request.Form["Date"];
                int bookingDay = int.Parse(DateTime.Parse(date).Day.ToString());
                int bookingMonth = int.Parse(DateTime.Parse(date).Month.ToString());
                int bookingYear = int.Parse(DateTime.Parse(date).Year.ToString());

                List<string> bookedSlots = _context.Appointments.Where(x => x.BookingDay == bookingDay && x.BookingYear == bookingYear && x.BookingMonth == bookingMonth).Select(x=>x.BookingTime).ToList();
                return Json(ControllerReturn<string>.ReturnList(bookedSlots));
            }
            catch(Exception e)
            {
                Logger.Error(e, ErrorCode.AB_BK_005, MessageHandler.getMessage(ErrorCode.AB_BK_005));
                return Json(ControllerReturn<bool>.ReturnError(ReturnStatus.Status.FAILED, ErrorCode.AB_BK_005,e, MessageHandler.getMessage(ErrorCode.AB_BK_005)));
            }
        }
    }
}
