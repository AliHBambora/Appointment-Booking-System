using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Appointment_booking_system.utils
{
    public class ReturnStatus
    {
        public enum Status
        {
            FAILED,
            BADREQUEST,
            UNAUTHORIZE,
            CONFLICT
        }

        public static HttpStatusCode getHttpStatusCode(ReturnStatus.Status status)
        {
            switch (status)
            {
                case Status.FAILED:
                    return HttpStatusCode.InternalServerError;
                case Status.BADREQUEST:
                    return HttpStatusCode.BadRequest;
                case Status.UNAUTHORIZE:
                    return HttpStatusCode.Unauthorized;
                default:
                    return HttpStatusCode.InternalServerError;
            }
        }
    }
}