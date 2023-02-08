using Appointment_booking_system.ExceptionHandling;
using GEAweb_API.Common.GEAException;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Appointment_booking_system
{
    public class Logger
    {

        public static void Error(string message)
        {
            Log.Error(message);
        }

        /// <summary>
        /// Write a log event with the Error level.
        /// </summary>
        /// <param name="messageTemplate">Message template describing the event.</param>
        /// <param name="propertyValues">Objects positionally formatted into the message template.</param>
        /// <example>
        /// Log.Error("Failed {param} records.", paramValue);
        /// </example>

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);
        }


        public static void Error(Exception exception, ErrorCode errorCode, string message)
        {
            string msg = "{ErrorCode} :" + message;
            Log.Error(exception, msg, errorCode.ToString());
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }

        public static void Debug(string messageTemplate, params object[] propertyValues)
        {
            Log.Debug(messageTemplate, propertyValues);
        }


    }


}
