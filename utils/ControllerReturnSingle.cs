using Appointment_booking_system.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GEAWebAPI.Controllers.Service
{
    public class ControllerReturnSingle<T> : ControllerReturn<T>
    {
        public T data { get; set; }

        public ControllerReturnSingle(string errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public ControllerReturnSingle()
        {

        }
    }
}