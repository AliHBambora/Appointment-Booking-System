using Appointment_booking_system.ExceptionHandling;
using Appointment_booking_system.utils;
using GEAWebAPI.Controllers.Service;
using System.Net;


namespace GEAweb_API.Common.GEAException
{
    public class GeneralException : Exception
    {
        public GeneralException(HttpStatusCode code, ControllerReturn<Object> service, string message, Exception innerException)
            : base(message, innerException)
        {
            serviceReturn = service;
            statusCode = code;
        }

        public GeneralException(ReturnStatus.Status status, ErrorCode code, string message, Exception innerException) : base(message, innerException)
        {
            serviceReturn = new ControllerReturn<object>(code == 0 ? "" : code.ToString(), message);
            statusCode = ReturnStatus.getHttpStatusCode(status);
        }

        public GeneralException(ReturnStatus.Status status, ErrorCode code, string message) : base(message)
        {
            serviceReturn = new ControllerReturn<object>(code == 0 ? "" : code.ToString(), message);
            statusCode = ReturnStatus.getHttpStatusCode(status);
        }


        public ControllerReturn<Object> serviceReturn { get; set; }
        public HttpStatusCode statusCode { get; set; }


    }
}